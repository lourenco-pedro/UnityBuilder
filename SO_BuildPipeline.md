Estrutura: [UnityBuilder](obsidian://open?vault=myVault&file=Unity%20Builder).SO_BuildPipeline
Herança: ScriptableObject

Classe responsável por conter informações de como a build deverá ser realizada.

O [BuildCmd](obsidian://open?vault=myVault&file=BuildCmd) acessa a SO_BuildPipeline definida e roda a função `Build()`. Salvando a build gerada no caminho definido no environment `UNITY_BUILDER_ROOT`.

 > 
 > É necessário que exista um *environment variable* criado com o nome `UNITY_BUILDER_ROOT` especificando o caminho que a build será gerada.
 > 
 > Se não existir nenhum *environment variable* com este nome, a build será cancelada, retornando `BuildResult.INVALID_ENVIROMENTS`

Ao fim da build, o arquivo é salvo dentro do caminho especificado pela variável de ambiente `UNITY_BUILDER_ROOT` com o nome `build_temp`.

### Variáveis

Privadas

|Nome|Descrição|
|----|-----------|
|\_target|A `BuildTarget` que a build será gerada.|
|\_options|Configurações adicionais que esta build poderá ter.|
|\_scenesInBuild|Cenas que irão conter nesta build. A primeira cena que irá carregar na build deverá ser o primeiro item deste Array.|

### Funções

Públicas

|Nome|Descrição|
|----|-----------|
|Build|Inicia o processo de build da Unity através do comando `BuildPipeline.BuildPlayer`. Ao fim da build, esta função retorna um resultado do tipo SO_BuildPipeline.[BuildResult](BuildResult.md)|
