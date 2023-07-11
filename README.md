# Conector nativo para o app Smart View da Totvs

Para configurar a aplicação no ambiente local é necessário apenas clonar o repositório para uma pasta e em seguida rodar a sequência de comandos abaixo:

1. Abra o terminal, podendo ser o powershell, cmd ou qualquer outro;
2. Navegue até a pasta onde o repositório foi clonado;
3. Execute o comando **docker build -t local_native_provider:latest --build-arg APP_VERSION=1.0.0 .** e aguarda a criação da imagem;
4. Execute o comando **docker compose up -d** e aguarde o container do app e do postgres subir;

Após executar esses comandos é necessário restaurar o banco de dados que será utilizado pela aplicação, então para isso você deverá rodar a sequência de comandos abaixo:

**Copia o backup do banco de dados para dentro do container do postgres**

`docker cp ./bikestores-bkp.sql postgres_native_provider:/tmp`

**Cria o banco de dados**

`docker exec -it postgres_native_provider psql -U postgres -c "create database bikestores"`

**Restura o banco de dados utilizando o arquivo de backup**

`docker exec -it postgres_native_provider psql -U postgres -d bikestores -f /tmp/bikestores-bkp.sql`

---

Feito esses passos a aplicação estará pronta para ser executada, bastando apenas executar o comando **docker compose stop** e **docker compose start** para reiniciá-la.

A rota de well-known fica disponível em http://localhost:8080/api/local-native-provider/.well-known/bikestores/connector.
