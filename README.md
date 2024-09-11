# OpenWeatherMapCLI

Assumptions:
- Program is being tested on a Windows machine (This could work on other machines, but would need modifications)

Run instructions:
- Open Solution in Visual Studio
- Add API key in OpenWeatherMapCLI/OpenWeatherMap/OpenWeatherMapHelper.cs
- Build both projects in Debug
- Open Test Explorer
- Run

Further possible work:
- Code only handles simple 5 digit zip codes, could be extended to handle zip codes with extensions
- Tests only handle single inputs, but the CLI program supports multiple calls in a single execution
- Make tests platform agnostic - Currently points to the CLI executable with hardcoded path
- More robust error messages - Currently catches all exceptions and maps to a common error response
