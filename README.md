A package developed by me to streamline Unity build generation through CMD commands for automated builds using Jenkins.

### Summary

* UnityBuilder
  * [BuildCmd](#buildcmd)
  * [SO_BuildPipeline](#so_buildpipeline)


# BuildCmd

Structure: [UnityBuilder](obsidian://open?vault=myVault&file=Unity%20Builder).BuildCmd
Inheritance: ScriptableObject

Class responsible for building the project using a pipeline defined in its instance. This class loads an instance of itself and uses the 'pipeline' field to determine which build pipeline to use when generating a build.

There should be only one instance of this class, created through the Unity command menu `right-click > Create > UnityBuilder > BuildCmd`. This object should be saved inside a `Resources/` folder, and its name cannot be changed.

### Variables

Public

|Name|Description|
|----|-----------|
|pipeline|SO_BuildPipeline used to generate build.|

### Static functions

Public

|Name|Description|
|----|-----------|
|Build|Function to be called when using the `-executeMethod` cmd command. This function loads the pipeline that will be used when generating a new build.|



# SO_BuildPipeline

Structure: [UnityBuilder](obsidian://open?vault=myVault&file=Unity%20Builder).SO_BuildPipeline
Inheritance: ScriptableObject

Class responsible for containing information on how the build should be carried out.

The [BuildCmd](obsidian://open?vault=myVault&file=BuildCmd) accesses the defined SO_BuildPipeline and runs the `Build()` function, saving the generated build to the path specified in the `UNITY_BUILDER_ROOT` environment.

> It is necessary to have an environment variable created with the name `UNITY_BUILDER_ROOT` specifying the path where the build will be generated.
> If there is no environment variable with this name, the build will be canceled, returning `BuildResult.INVALID_ENVIRONMENTS`.

At the end of the build, the file is saved inside the path specified by the `UNITY_BUILDER_ROOT` environment variable with the name `build_temp`.

### Variables

Private

| Name           | Description                                           |
| -------------- | ----------------------------------------------------- |
| \_target       | The `BuildTarget` for which the build will be generated. |
| \_options      | Additional settings this build may have.              |
| \_scenesInBuild| Scenes to be included in this build. The first scene to load in the build should be the first item in this array. |

### Functions

Public

| Name  | Description                                           |
| ----- | ----------------------------------------------------- |
| Build | Initiates the Unity build process using the `BuildPipeline.BuildPlayer` command. At the end of the build, this function returns a result of type SO_BuildPipeline.[BuildResult](#buildresult)|

# BuildResult

Structure: [UnityBuilder](obsidian://open?vault=myVault&file=Unity%20Builder).[SO_BuildPipeline](obsidian://open?vault=myVault&file=SO_BuildPipeline).BuildResult
Inheritance: No

Enum that contains post-build information. With this enum, you can determine whether the build was successful or if it was canceled due to some factor.

### Values

| Name                 | Description                                           |
| -------------------- | ----------------------------------------------------- |
| SUCCESS              | The build was generated successfully.                 |
| INVALID_ENVIRONMENTS | The build was canceled due to some environment variable issue. Some variables may not exist and/or contain incorrect values. |
