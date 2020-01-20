# bcdevexchange
This Repository is for the BCDevExchange website.

# Local development.
This website is generated using .Net Core 2.2

## Windows

## Mac

## Linux
Install dotnet 2.2 Core if it's not already installed
https://docs.microsoft.com/en-us/dotnet/core/install/linux-package-manager-ubuntu-1804

Make certaing the version is 2.2 by running

`dotnet --version`

If version 3.* is running, it will need to be downgraded

To run the site locally execute the command, `dotnet run`, in the bcdevexchange folder.

It will be hosted on the url `http://localhost:5000`

If you require hot reloading the web page will need to be run through VS codes debug and run feature as Razor and .cshtml files are excluded from `dotnet   watch run` functionality.