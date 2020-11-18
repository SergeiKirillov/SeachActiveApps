using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
 

class www
{
}


class server
{
    TcpListener Listener;

    public void start()
    {
        int port = 80;
        Listener = new TcpListener(IPAddress.Any, port); //Создаем "слушателя" для указанного порта
        Listener.Start(); //Запускаем его

        while (true)
        {
            new Client(Listener.AcceptTcpClient()); //Принимаем новых клиентов
        }

    }

    ~server() // Остановка сервера
    {
        if (Listener != null) //Если "слушатель" был создан
        {
            Listener.Stop();  //Остановим его
        }

    }
}

class Client
{
    public Client(TcpClient Client)
    {
        //Код простой интернет странички
        string html = "<html><body><h1>It Works!</h1></body></html>";

        //Ответ сервера
        string Str = "HTTP/1.1 200 OK\nContent-type: text/html\nContent-Length:" + html.Length.ToString() + "\n\n" + html;

        //ответ в массив байт
        byte[] Buffer = Encoding.ASCII.GetBytes(Str);

        //Отправим его клиенту
        Client.GetStream().Write(Buffer, 0, Buffer.Length);

        Client.Close();


    }
}
