# pwned-password-check-net
Simple .net core command line utility for checking your pwned accounts against Troy Hunt's Have I Been Pwned? service. Export accounts into CSV file and run pwned-password-check-net.dll.

> dotnet pwned-password-check-net.dll -p accounts.csv -l KeePass

# Install
## Windows
### Build from source code
Clone this repository and compile with dotnet tooling. (or visual studio code)
### Download
Download from AppVeyor continuous integration.
[Latest build](https://ci.appveyor.com/api/projects/hopp/pwned-password-check-net/artifacts/pwned-password-check-net.zip?branch=master)

## Linux
    sudo snap install dotnet-sdk --classic
    mkdir ./pwned && cd ./pwned
    wget https://ci.appveyor.com/api/projects/hopp/pwned-password-check-net/artifacts/pwned-password-check-net.zip?branch=master -O master.zip
    unzip master.zip
    chmod 644 *

# Prepare accounts
## Linux
    cat << EOF > accounts.csv
    Account,LoginName,Password,WebSite,Comments
    my_gaccount,firstname.lastname@gmail.com,-,gmail.com,email provider
    my_yaccount,firstname.lastname@yahoo.com,-,yahoo.com,email provider
    EOF

## Manual
Export your passwords to csv file with following structure:

| Account | LoginName | Password | WebSite | Comments |
| --- | --- | ---| --- | --- |
| My favourite service|your_account@your_domain.com|password|www.myfavouriteservice.nowhere|comment|

Note: This is default export from KeePass.

# Run

## Windows
    dotnet pwned-password-check-net.dll -p path_to_csv -l KeePass

## Linux
    dotnet-sdk.dotnet pwned-password-check-net.dll -p path_to_csv -l KeePass

## Parameters

There are two parameters required:

|Parameter|Long Name|Description|
|---|---|---|
|p|path| Path to your csv file|
|l|layout| Choose Layout. Options: KeePass|


# Output

This tool will group output according to your logins and for each login tries if password was compromised.

```
Login: your_login@your_domain.com, Breaches: breach1, breach2
Possible pwned password - Account: My Favourite Page, Password: abcd123
....
Login: firstname.lastname@gmail.com, Breaches: Adobe, Gaadi, GeekedIn, LinkedIn, MDPI, Modern Business Solutions, MPGH, NemoWeb, Onliner Spambot, River City Media Spam List, Ticketfly, Torrent Invites
Possible pwned password - Account: my_account, Password: pwd
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
