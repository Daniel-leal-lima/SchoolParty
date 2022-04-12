using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class DBteste : MonoBehaviour
{
    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;

    [SerializeField]
    private string dbName = "URI=File:DbTeste.db";

    [SerializeField]
    private int idTest;


    // Start is called before the first frame update
    void Start()
    {
        Connection();
    }

    public void Connection(){
        connection = new SqliteConnection(dbName);//definindo a conexão com a  tabela
        command = connection.CreateCommand();//criando o comando para passar os comandos
        connection.Open(); //abrindo conexao

        //comando
        string comandoCriarTabela = "CREATE TABLE IF NOT EXISTS TabelaTeste(id INTEGER PRIMARY KEY AUTOINCREMENT, nome VARCHAR(50), dano INT);";
        //INTEGER PRIMARY KEY AUTOINCREMENT começa com 1 e não 0
        command.CommandText = comandoCriarTabela; //passando comando
        command.ExecuteNonQuery();///executando comando

        // connection.Close(); //fechando conexao
        Debug.Log("Conectou");

    }

    public void Insert(){
        string comandoInserir = "INSERT INTO TabelaTeste (nome, dano) VALUES ('Espada', 4);" +
                                "INSERT INTO TabelaTeste (nome, dano) VALUES ('Arco', 3);" +
                                "INSERT INTO TabelaTeste (nome, dano) VALUES ('Faca', 2);" +
                                "INSERT INTO TabelaTeste (nome, dano) VALUES ('Machado', 5);";
        //"string'+variavel+'string"

        command.CommandText = comandoInserir;
        command.ExecuteNonQuery();
        Debug.Log("Inseriu");
    }

    public void Select(){
        string comandoSeleciona = "SELECT id, nome, dano FROM TabelaTeste WHERE id like " + idTest + ";";
        command.CommandText = comandoSeleciona;

        //para ler do banco ou comandos de leitura
        reader = command.ExecuteReader(); //retorna um objeto do tipo reader, que vai pro while para pegar read
        while (reader.Read()){

            //indice referente a posição do select
            int id = reader.GetInt32(0);
            string nome = reader.GetString(1);
            int dano = reader.GetInt32(2);

            Debug.Log("Id: " + id + " Arma: " + nome + " Dano: " + dano);
        }
        reader.Close();
    }
}
