# pwned-password-check-net
Simple .net core command line utility for checking your pwned passwords against Troy Hunt's Have I Been Pwned? service.


# Usage
## Build from source code
Clone this repository and compile with dotnet tooling. (or visual studio code)
## Download
Download from AppVeyor continuous integration.
[Latest build](https://ci.appveyor.com/api/projects/hopp/pwned-password-check-net/artifacts/pwned-password-check-net.zip?bramch=master)

## Export passwords
Export your passwords to csv file, with structure

| Account | LoginName | Password | WebSite |
| ---     | --- | ---| --- |
| My favourite service|your_account@your_domain.com|password|www.myfavouriteservice.nowhere
This is default export from KeePass.
## Run
There are two parameters
|Paremeter|Long Name|Description|
|---|---|---|
|p|path| Path to your csv file|
|l|layout| Layout

> Currently only layout implemented is KeePass layout

Command line 
> dotnet pwned-password-check-net.dll -p path_to_csv -l KeePass
# Sample output

This tool will group output according to your logins and for each login tries if password was compromised.

```
Login: your_login@your_domain.com, Breaches: breach1, breach2
Possible pwned password - Account: My Favourite Page, Password: abcd123
....
....
Login: your_another_login@your_another_domain.com, Breaches: breach3, breach4
Possible pwned password - Account: Some Page, Password: 123abcd
....
....
```
# Remarks
This tool uses c# implementation of Troy's Hunt pwned api [SharpPwned.NET](https://github.com/FaithLV/SharpPwned.NET).

Mathematical property called [k-Anonymity](https://www.troyhunt.com/ive-just-launched-pwned-passwords-version-2/#cloudflareprivacyandkanonymity) is used to check if the password was compromised.

1. None of your passwords ever leaves your computer.
2. We can't say if combination of your account and the password was compromised. If account is listed in the output, we are sure that account was compromised. If password was listed, we are sure that this password was compromised.

If you have pretty unique password and is listed as compromised, there is quite a chance, it is your account. 

Act accordingly.

And always use password manager.

# Issues and PRs

Please report [issues](https://github.com/hopp/pwned-password-check-net/issues). Pull Requests are welcomed !
