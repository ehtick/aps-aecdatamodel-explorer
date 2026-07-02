# AEC Data Model Explorer

![platforms](https://img.shields.io/badge/platform-windows%20%7C%20osx%20%7C%20linux-lightgray.svg)
[![.net](https://img.shields.io/badge/net-10.0-blue.svg)](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
[![license](https://img.shields.io/:license-mit-green.svg)](https://opensource.org/licenses/MIT)

An [Autodesk Platform Services](https://aps.autodesk.com) sample that uses the [GraphiQL](https://github.com/graphql/graphiql) interface to explore the [AEC Data Model GraphQL API](https://aps.autodesk.com/en/docs/aecdatamodel/v1/developers_guide/overview/). It also integrates the APS Viewer to contextualize query results directly in the 3D model.

![GraphiQL](./readme/GraphiQL.png)

## Prerequisites

- [APS credentials](https://aps.autodesk.com/en/docs/oauth/v2/tutorials/create-app) — a registered APS application with the **Data Management** API enabled and a **3-legged OAuth** callback URL configured
- [.NET 10](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- A terminal such as [PowerShell](https://learn.microsoft.com/en-us/powershell/scripting/overview) or [bash](https://en.wikipedia.org/wiki/Bash_(Unix_shell))

> We recommend using [Visual Studio Code](https://code.visualstudio.com), which provides a built-in terminal and great C# support via the [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) extension.

## Setup & Run

- Clone this repository:
  ```
  git clone https://github.com/autodesk-platform-services/aps-aecdatamodel-explorer
  ```
- Go to the project folder:
  ```
  cd aps-aecdatamodel-explorer
  ```
- Create an _appsettings.Development.json_ file in the project folder (if it does not exist already) and populate it with the snippet below, replacing the placeholder values with your APS app credentials. Make sure the callback URL matches exactly what is configured in your APS application:
  ```json
  {
    "APS_CLIENT_ID": "<your-client-id>",
    "APS_CLIENT_SECRET": "<your-client-secret>",
    "APS_CALLBACK_URL": "http://localhost:8080/api/auth/callback"
  }
  ```
- Restore dependencies and run:
  ```
  dotnet run
  ```
- Open http://localhost:8080 in your browser and sign in with your Autodesk account

> When using [Visual Studio Code](https://code.visualstudio.com), you can run and debug the application by pressing `F5`.

## Features

Once signed in, the sample loads the GraphiQL interface pre-loaded with step-by-step tutorial queries across multiple tabs (GetHubs, GetProjects, GetElementGroupsByProject, GetElementsFromCategory).

![Queries](./readme/Queries.png)

The built-in documentation browser lets you explore the full AEC Data Model schema.

![Docs](./readme/Docs.png)

The Viewer is controlled by the toggle switch in the header. It loads the model based on an item or version URN you provide.

![workflow](./readme/workflow.gif)

The **AECDMFilterExtension** matches External IDs from the last GraphQL query response to elements in the loaded model and isolates them in the Viewer.

![Tips](./readme/Tips.png)

## Tips & Tricks

1. **You must be signed in** to use any feature in this sample.
2. **Item/Version ID field** accepts either an item ID or a version ID. If you provide an item ID, the latest version is loaded automatically.
3. **Make sure the Design ID and the Item/Version ID are related to the same model.**
4. **The filter extension** looks for source IDs in the last GraphQL query response, then finds matching External IDs in the loaded model to isolate those elements.

## Troubleshooting

1. **Cannot load the Viewer**: make sure you are signed in and using a valid item or version URN.
2. **Filter Extension does not highlight elements**: make sure the Item/Version ID is compatible with the Design ID used in your GraphQL queries.
3. **Authentication errors**: verify that the callback URL in _appsettings.Development.json_ exactly matches the one registered in your APS application settings.

## License

This sample is licensed under the terms of the [MIT License](http://opensource.org/licenses/MIT). Please see the [LICENSE](LICENSE) file for more details.
