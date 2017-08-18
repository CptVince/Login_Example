# Login Example

Put the `login.php` file into your htdocs folder and change the MYSQL settings if required.

# Start the program
May change the values in `Program.cs` to use the url.

``` CSharp
const string logonPage = "http://localhost/login.php";
```

As well as the login data for the test-user.

# In the database one user is created

``` SQL
CREATE TABLE `account` (
  `Id` int(11) NOT NULL,
  `username` varchar(255) NOT NULL,
  `sha_pass_hash` varchar(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `account` (`Id`, `username`, `sha_pass_hash`) VALUES
(1, 'TestUser', 'dc8cb7f3db2e9e8b5cea925dc7d39d53e707d2f7');


ALTER TABLE `account`
  ADD PRIMARY KEY (`Id`);


ALTER TABLE `account`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
```

## Output should look like

``` Bash
Valid Login results in:
response message: TESTUSER:DC8CB7F3DB2E9E8B5CEA925DC7D39D53E707D2F7

Invalid Login results in:
User is unauthorized: The remote server returned an error: (401) Unauthorized.
```
