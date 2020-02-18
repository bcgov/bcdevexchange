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

# Running the `Learning` page locally

In order for the learning page to load locally a `.env` file must be created.  The `.env` is listed in the `.gitignore` file to prevent secrets from getting committed and made public.  To generate the `.env` file save a copy of `.env-example`, and rename it `.env`.  Then get a copy of the BEARER_TOKEN needed from the site administrator.

The Learning page should now be accessible locally.  If it does not render properly in debugging mode, try running the project from the terminal using the command `dotnet run`.

# Contributing to the page.

Feel like helping out?  Spot a problem?  Feel free to fork the repos and make a pull request, or raise an issue for us to review.