using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            //Первый вариант - новый поток для каждого входящего клиента
            TcpClient clientWWW = Listener.AcceptTcpClient();
            //Создаем поток
            Thread threadClient = new Thread(new ParameterizedThreadStart(ClientThread));
            //И запускаем поток
            threadClient.Start(clientWWW);

            
        }

    }

    static void ClientThread(object StateInfo)
    {
        // Просто создаем новый экземпляр класса Client и передаем ему приведенный к классу TcpClient объект StateInfo
        new Client((TcpClient)StateInfo);
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
        ////Вариант 1 - Простая станичка -
        ///
        ////Код простой интернет странички
        //string html = "<html><body><h1>It Works!</h1></body></html>";

        ////Ответ сервера
        //string Str = "HTTP/1.1 200 OK\nContent-type: text/html\nContent-Length:" + html.Length.ToString() + "\n\n" + html;

        ////ответ в массив байт
        //byte[] Buffer = Encoding.ASCII.GetBytes(Str);

        ////Отправим его клиенту
        //Client.GetStream().Write(Buffer, 0, Buffer.Length);

        //Client.Close();


        ////Вариант 2 - Более навороченная страница - 

        string Request = ""; // Объявим строку, в которой будет хранится запрос клиента
        byte[] BufferRequest = new byte[1024]; // Буфер для хранения принятых от клиента данных
        int CountRequest;  // Переменная для хранения количества байт, принятых от клиента

        while ((CountRequest=Client.GetStream().Read(BufferRequest,0,BufferRequest.Length))>0) // Читаем из потока клиента до тех пор, пока от него поступают данные
        {
            Request += Encoding.ASCII.GetString(BufferRequest, 0, CountRequest); // Преобразуем эти данные в строку и добавим ее к переменной Request

            if (Request.IndexOf("\r\n\r\n")>=0 || Request.Length>4096) // Запрос должен обрываться последовательностью \r\n\r\n, либо обрываем прием данных сами, если длина строки Request превышает 4 килобайта
            {
                break;
            }
        }

        Match MathRequest = Regex.Match(Request, @"^\w+\s+([^\s\?]+)[^\s]*\s+HTTP/.*|"); //Парсим строку запроса с использованием регулярных выражений, при этом отсекаем все переменные GET-запроса

        if (MathRequest == Match.Empty) // Если запрос не удался
        {
            SendError(Client, 400); // Передаем клиенту ошибку 400 - неверный запрос
            return;
        }

        string URIRequest = MathRequest.Groups[1].Value; // Получаем строку запроса
        URIRequest = Uri.UnescapeDataString(URIRequest); // Приводим ее к изначальному виду, преобразуя экранированные символы. Например, "%20" -> " "

        if (URIRequest.IndexOf("..") >= 0) // Если в строке содержится двоеточие, передадим ошибку 400
        {
            SendError(Client, 400);
        }

        if (URIRequest.EndsWith("/")) // Если строка запроса оканчивается на "/", то добавим к ней index.html
        {
            URIRequest += "index.html";
        }

        string FilePath = "www/" + URIRequest;

        if (!File.Exists(FilePath)) // Если в папке www не существует данного файла, посылаем ошибку 404
        {
            SendError(Client, 404);
            return;
        }

        string Extension = URIRequest.Substring(URIRequest.LastIndexOf('.')); // Получаем расширение файла из строки запроса
        string ContentType = ""; // Тип содержимого

        switch (Extension)  // Пытаемся определить тип содержимого по расширению файла
        {
            case ".htm":
            case ".html":
                ContentType = "text/html";
                break;
            case ".css":
                ContentType = "text/stylesheet";
                break;
            case ".js":
                ContentType = "text/javascript";
                break;
            case ".jpg":
                ContentType = "image/jpeg";
                break;
            case ".jpeg":
            case ".png":
            case ".gif":
                ContentType = "image/" + Extension.Substring(1);
                break;
            default:
                if (Extension.Length > 1)
                    {
                        ContentType = "application/" + Extension.Substring(1);
                    }
                else
                    {     
                        ContentType = "application/unknown";
                    }
                break;
        }

        // Открываем файл, страхуясь на случай ошибки
        FileStream FS;
        try
        {
            FS = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
        catch (Exception)
        {
            // Если случилась ошибка, посылаем клиенту ошибку 500
            SendError(Client, 500);
            return;
        }
        
         // Посылаем заголовки
         string Headers = "HTTP/1.1 200 OK\nContent-Type: " + ContentType + "\nContent-Length: " + FS.Length + "\n\n";
                byte[] HeadersBuffer = Encoding.ASCII.GetBytes(Headers);
                Client.GetStream().Write(HeadersBuffer, 0, HeadersBuffer.Length);
        
         // Пока не достигнут конец файла
         while (FS.Position < FS.Length)
         {
                // Читаем данные из файла
                CountRequest = FS.Read(BufferRequest, 0, BufferRequest.Length);
                // И передаем их клиенту
                Client.GetStream().Write(BufferRequest, 0, CountRequest);
         }
        
         // Закроем файл и соединение
        FS.Close();
        Client.Close();






    }

    private void SendError(TcpClient Client, int Code)
    {
        // Получаем строку вида "200 OK"
        // HttpStatusCode хранит в себе все статус-коды HTTP/1.1
        string CodeStr = Code.ToString() + " " + ((HttpStatusCode)Code).ToString();
        // Код простой HTML-странички
        string Html = "<html><body><h1>" + CodeStr + "</h1></body></html>";
        // Необходимые заголовки: ответ сервера, тип и длина содержимого. После двух пустых строк - само содержимое
        string Str = "HTTP/1.1 " + CodeStr + "\nContent-type: text/html\nContent-Length:" + Html.Length.ToString() + "\n\n" + Html;
        // Приведем строку к виду массива байт
        byte[] Buffer = Encoding.ASCII.GetBytes(Str);
        // Отправим его клиенту
        Client.GetStream().Write(Buffer, 0, Buffer.Length);
        // Закроем соединение
        Client.Close();
    }


}
