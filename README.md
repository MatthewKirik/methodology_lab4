# Task list manager

This project is a simple console application for managing calendar tasks that describe and define a specific scheduled event over time. The user has the ability to manage tasks, keep track of their deadlines and add a short description of them.

## Quick Start

**Firstly, you need to install .NET Core on your PC** ([instructions here](https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu))

Example (Ubuntu 20.04 LTS):
```
$ wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb

$ sudo dpkg -i packages-microsoft-prod.deb

$ rm packages-microsoft-prod.deb
```

**To build the project you need to go forward into solution directory and run:**

```
$ dotnet restore

$ dotnet build
```

**You can simply run tests with:**

```
$ dotnet test
```
## Command line arguments

To run the program specifying command args, you need to go forward into ConsoleView directory and then run the program like this:

```
dotnet run --show
```

See ([design document](./docs/design-doc.pdf)) for more information about project and command args.

## License

MIT
