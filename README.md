# Introduction 
This project is for brain storming sales and is based on dotnet core 3.1 and react running under iis,
this porject was done years ago and when get time will upgrade to react to use typescript
and dotnet 6 + tye + kubenetes

### Getting Started

1.	Installation process
2.	Software dependencies
3.	Latest releases
4.	API references

#### Installation process

In order to develop and get running quickly you will need the following software

1. Dotnet core 3.1
2. IIS
3. Certificates
4. Update host file
5. Visual Studio 2019
6. Visual Studio Code
7. Docker for Windows
8. Application Request Routing 
9. node js (npm)
10. Tye [instructions here](https://github.com/dotnet/tye)


#### Software dependencies

The base build is at a lower version than is required for docker and windows 
will need to be upgraded to build 10.0.18862 or higher.

#### Application Request Routing

AAR is required to route base address to the api services more easily and 
requires not knowing ports when communicating through http clients.
[instructions here](https://techcommunity.microsoft.com/t5/iis-support-blog/setup-iis-with-url-rewrite-as-a-reverse-proxy-for-real-world/ba-p/846222)

#### Update host file

Add the following to your host file in C:\Windows\System32\drivers\etc

127.0.0.1 sale.dab.localhost

127.0.0.1 search.api.sale.dab.localhost

127.0.0.1 menu.api.sale.dab.localhost

127.0.0.1 identity.sale.dab.localhost

#### IIS Setup

Create a site called vip and set base directory to c:\inetpub\wwwroot and 
then the bindings host name to sale.dab.localhost then setup AAR reverse 
proxy to use either port 3000 if devoping in react / visual studio code or 
port 4000 if you want to test it running in dotnet core.

Create a site called searchapi and set base directory to c:\inetpub\wwwroot and 
then the bindings host name to search.api.sale.dab.localhost then setup AAR reverse 
proxy to use port 5000.

Create a site called menuapi and set base directory to c:\inetpub\wwwroot and 
then the bindings host name to menu.api.sale.dab.localhost then setup AAR reverse 
proxy to use port 5010.

Create a site called salesindentity and set base directory to c:\inetpub\wwwroot and 
then the bindings host name to menu.api.sale.dab.localhost then setup AAR reverse 
proxy to use port 5500.

don't forget to set conditions on rewrite rules

example rules

[Url Rewrite](urlRewrite.PNG)

[Url Conditions](urlRewriteCondition.PNG)

You will also need to follow these instructions to the letter
before the frontend will work correctly but make sure the server variables are done
at the root of IIS

[React compression](https://techcommunity.microsoft.com/t5/iis-support-blog/iis-acting-as-reverse-proxy-where-the-problems-start/ba-p/846259)

Also on the main site you will need to turn off and setting for proxy
reverse rewriting TBA
 
#### Certificates

In order to serve over https you will need to create certs for IIS use these commands
and use the subject when binding to https in IIS

run powershell as Admin and use these commands

$pw = ConvertTo-SecureString -String "Pa55w0rd" -Force -AsPlainText

New-SelfSignedCertificate -certstorelocation cert:\localmachine\my -subject *.dab.localhost -dnsname sale.dab.localhost,*.dab.localhost,menu.api.sale.dab.localhost,search.api.sale.dab.localhost,identity.sale.dab.localhost,account.dab.localhost

a thumb print will be prodcued that will be required in the next command

Export-PfxCertificate -cert cert:\localMachine\my\[Paste thumb  print here] -FilePath $env:USERPROFILE\Desktop\Cert_sale.pfx -Password $pw

Once this is file is created import this file into certificate manager under 

Trusted Root Certification Authorities

now configure iis for https for each site required by selecting *.dab.localhost


If you add more services/urls then add to New-SelfSignedCertificate

and follow the above steps


# Contribute
Please feel free to update this and contribute

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
