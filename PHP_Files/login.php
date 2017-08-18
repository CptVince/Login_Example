<?php
session_start();

define ('MYSQL_USER', 'root');
define('MYSQL_PASSWORD', '');
define('MYSQL_HOST', 'localhost');
define('MYSQL_DATABASE', 'auth');

$pdoOptions = array(
    PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
    PDO::ATTR_EMULATE_PREPARES => false
);

$pdo = new PDO(
    "mysql:host=" . MYSQL_HOST . ";dbname=" . MYSQL_DATABASE,
    MYSQL_USER,
    MYSQL_PASSWORD,
    $pdoOptions
);

if (!isset($_POST['username']) || !isset($_POST['sha_pass_hash'])) {
  header("HTTP/1.1 401 Unauthorized");
  session_destroy();
  return;
}

function encryptsha($username, $password) {
  $usr = strtoupper($username);
  $pwd = strtoupper($password);

  return sha1($usr . ':' . $pwd);
}

$username = $_POST["username"];
$password = encryptsha($username, $_POST["sha_pass_hash"]);

$sth = $pdo->prepare('SELECT *
FROM account
WHERE username = :username AND sha_pass_hash = :password');
$sth->bindValue(':username', $username, PDO::PARAM_STR);
$sth->bindValue(':password', $password, PDO::PARAM_STR);
$sth->execute();
$result = $sth->fetch(PDO::FETCH_ASSOC);

if ($result['username'] != $username || $result["sha_pass_hash"] != $password) {
  header("HTTP/1.1 401 Unauthorized");
  session_destroy();
  return;
}

header("HTTP/1.1 200 Ok");
$_SESSION['username'] = $username;
echo strtoupper($username) . ":" . strtoupper($password);

session_destroy();
