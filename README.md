# Chatee-Messenger
This is C# based application for chatting made with WPF, WCF and Entity Framework with SQLite database. This messenger has basic functions you would expect from such programs: creating new account, signing in existing account, finding already existing users, sending text messages, images and files up to 200MB, donwloading sent files and images.

![Chat page](https://user-images.githubusercontent.com/59961299/72526011-1c440800-386e-11ea-8c8b-05faa8d5802c.png)
## Gallery
![Sign in](https://user-images.githubusercontent.com/59961299/72526008-1bab7180-386e-11ea-8573-21c81adfd3ba.png)
![Sign up](https://user-images.githubusercontent.com/59961299/72526010-1c440800-386e-11ea-9a3e-43ca158a93be.png)
![User settings](https://user-images.githubusercontent.com/59961299/72526012-1c440800-386e-11ea-9244-f5b48592eec5.png)
## Architecture
![Application Structure](https://user-images.githubusercontent.com/59961299/72526594-77c2c580-386f-11ea-97eb-ccff86f76ca3.png)
## I have learnt
* OOP
* Projecting client-server application
* Developing of WCF Service
* Pros and cons of different bindings in WCF
* Creating DataContracts for sending and receiving information
* Developing client with MVVM pattern
* Making multi-thread application
* Storing passwords as a computed with MD-5 algorithm hash
## Features
* Registring of new user
* Signing in existing account
* Searching for already existing users
* Sending text message
* Sending file up to 200MB
* Sending image up to 200MB
* Downloading files and images from chats
* Changing user settings
## Getting Started
To use the application, firstly, you will need to copy the entire repository. Open bash, go to the folder where you would like to store files and write:
```
git clone https://github.com/vladyslavkhimich/Chatee-Messenger.git
```
To run the program you need to open Visual Studio and rebuild the whole project. After this you can find ChateeWPF.exe file in ChateeWPF/bin/Debug/ directory.
Similar approach you need to start a server, but in this case you need to go to Host/bin/Debug and find Host.exe file in this directory.
After this you can run program, but server will work on the local machine. If you want to host server on real existing ip address, you will need to change following files:
*[Host App.Config](https://github.com/vladyslavkhimich/Chatee-Messenger/blob/master/Host/App.config)
```
      <host>
          <baseAddresses>
            <add baseAddress="YOUR_HTTP_ADDRESS"/>
            <add baseAddress="YOUR_TCP_ADDRESS"/>
          </baseAddresses>
       </host>
```
*[ChateeWPF App.Config](https://github.com/vladyslavkhimich/Chatee-Messenger/blob/master/ChateeWPF/App.config)
```
      <endpoint address="YOUR_CLIENT_TCP_ADDRESS" binding="netTcpBinding"
                bindingConfiguration="NetTcpBinding_IService" contract="ServiceReference.IService"
                name="NetTcpBinding_IService">
```
### Prerequisities
You have to have installed ".NET desktop development" packages and .NET of version 4.6.1 To install them, you should open Visual Studio, then go to the upper tool-bar and choose "Tools" then "Get Tools and Features..." there you can find all you need.
## Built with
* C#
* WPF -Framework for UI
* WCF - Framework for developing services
* XAML - Markup language in WPF
* Entity Framework - For dealing with database
* Ninject v3.3.4 - NuGet Package
* Fody v4.2.1 - NuGet Package
* PropertyChangedFody v2.6.0 - NuGet Package
* System.Data.SQLite.Core v1.0.110 - NuGet Package
* Visual Studio 2019 - IDE
