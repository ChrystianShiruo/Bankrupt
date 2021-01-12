

O código está em Assets/Scripts

Para executar o projeto basta extrair a pasta assets e executar a cena em Assets/Scenes, feito na versão da Unity 2019.4.15f e provalvemente compatível com versões anteriores.
Alternativamente, para executar a partir de um novo projeto Unity:
	1. Extrair Pasta Assets/Scripts do arquivo (.zip) dentro da pasta Assets do novo projeto.
	2. Criar pasta resources e colocar nela o arquivo gameConfig.txt disponibilizado para o teste, tambem disponível em Assets/Resources do arquivo (.zip).
	3. Criar nova Cena e colocar o script Controller em un novo GameObject, os valores já deverão estar corretos para o teste, caso contrario os valores dos campos do inspetor são:
		-Start Money: 300;
		-Lap Reward: 100;
		-Round Limit: 1000;

A simulação será executada ao dar play na cena.
O output da última simulação será salvo dentro do projeto em Assets/LastExecutionResults em formato (.txt) e também aparecerá no console da Unity.
Observações:
	-O output da última simulação é salva logo ao término do teste, no entanto a Unity só atualiza a aba project ao fim da execução, portanto para ver o novo resultado pare a cena ou abra o arquivo LastExecutionResults.
	-O output em LastExecutionResults.txt é sobreescrito após cada execução do projeto.
	-O arquivo LastExecutionResults.txt presente no (.zip) é output da minha última simulação.