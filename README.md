# PushAll API for .Net

Бибилиотека реализует методы для отправки уведомлений через сервис PushAll

# Использование

1. [Создайте канал](https://pushall.ru)
2. Инициализируйте PushAllApi передав в параметрах идентификатор Вашего канала и ключ API 
3. Используйте методы для отправки сообщений

```csharp
PushAllApi pushAllApi = new PushAllApi(new PushAllOptions { ChannelId = xxxx, ApiKey = "d22e9f2fcdc8" });

MulticastParameters multicastParameters = new MulticastParameters
{
	Text = "Текст сообщения",
	Title = "Заголовок сообщения",
	Url = "https://google.com",
	Recipients = new ulong[] { xxxx, yyyy, zzzz }
};

ulong lid = await  pushAllApi.SendMulticastAsync(multicastParameters);
```
