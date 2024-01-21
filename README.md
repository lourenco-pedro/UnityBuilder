Uma package desenvolvida por mim para facilitar a geração de builds da Unity através de comandos cmd para automatização de builds utilizando o Jenkins.

### Estrutura

* UnityBuilder
  * [BuildCmd](#buildcmd)
  * [SO_BuildPipeline](#so_buildpipeline)


# BuildCmd

Estrutura: [UnityBuilder](obsidian://open?vault=myVault&file=Unity%20Builder).BuildCmd
Herança: ScriptableObject

Classe responsável por buildar o projeto utilizando uma pipeline definida em sua instância. Esta classe carrega um instância dela mesma e utiliza o campo pipeline para definir qual build pipeline usar na hora de gerar uma build.

Deverá haver apenas uma instância desta classe. Podendo ser criada através do menu de comandos da Unity `botão direito do mouse > Create > UnityBuilder > BuildCmd`. Este objeto deverá ser salvo dentro de uma pasta `Resources/`, e o seu nome não poderá ser alterado.

### Variáveis

Públicas

|Nome|Descrição|
|----|-----------|
|pipeline|SO_BuildPipeline utilizada para gerar a build.|

### Funções estáticas

Públicas

|Nome|Descrição|
|----|-----------|
|Build|Função que deve ser chamada quando estiver utilizando o comando `-executeMethod` do cmd. Esta função carrega a pipeline que será utilizada na hora de gerar uma nova build.|



# SO_BuildPipeline

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
|Build|Inicia o processo de build da Unity através do comando `BuildPipeline.BuildPlayer`. Ao fim da build, esta função retorna um resultado do tipo SO_BuildPipeline.[BuildResult](#buildresult)|


# BuildResult

Estrutura: [UnityBuilder](obsidian://open?vault=myVault&file=Unity%20Builder).[SO_BuildPipeline](obsidian://open?vault=myVault&file=SO_BuildPipeline).BuildResult
Herança: Não

Enum que contém informações pós builds. É com este enum que poderá saber se a build foi gerada com sucesso, ou se foi cancelada devido à algum fator.

### Valores

|Nome|Descrição|
|----|-----------|
|SUCCESS|A build foi gerada com sucesso.|
|INVALID_ENVIRONMENTS|A build foi cancelada devido à algum problema de variáveis de ambiente. Alguma variável pode não existir e/ou contém valores errados.|
