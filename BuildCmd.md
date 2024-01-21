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
