# Типизированные перечисления

## Мотивация к использованию
 - Сделать перечисления более выразительными 
 - Сделать перечисления более безопасными

## Выразительность 
Обычные перечисления

```cs
enum State
{
    Stand = 0,
    Sit,
    Walk,
    Run
}
```
не позволяют добавить к ним дополнителтные методанные просты способом без аттрибутов и рефлексии, 
например добавить признак перемещения 

### Реализация с помощью ITypedEnum 

```cs 

public class State : ITypedEnum<State> 
{
    public string Value { get; }
    
    public bool IsMoved { get; }
    
    private State(string value, isMoved) 
    {
        Value = value;
        IsMoved = isMoved
    }
    
    public static State Stand { get; } = new ("Stand", false);
    public static State Sit { get; } = new ("Sit", false);
    public static State Walk { get; } = new ("Walk", true);
    public static State Run { get; } = new ("Run", true);
}

class Person 
{
   string Id { get; set; }
   State State { get; set; } = State.Stand; 
}

```

### Примеры

- Получить перемещающиеся сущности из MongoDB
 
```cs
  var filter = Builder<Person>.Filter.In(x => x.State, TypedEnumFactory<State>.All.Where(s => s.IsMoved).ToArray()) 
  ```
- Получить по State из MongoDB
```cs
  var filter = Builder<Person>.Filter.Eq(x => x.State, State.Sit) 
  ```
```cs
  var filter = Builder<Person>.Filter.Where(x => x.State == State.Sit) 
  ```

## Безопасность

Больше не нужно использовать аттрибут [BsonRepresentation(BsonType.String)] для MongoDB
При исползовании BsonDocument больше не нужно писать ToString() у State как это было с enum 

Безопасная запись в базу  (короткая реализация на основе интерфейса)

```cs
public interface IState : ITypedEnum<State>
{
    public bool IsMoved { get; }
    
    public static ITypedEnumInterface Undefined { get; } = Undef.Instance;
    public static State Stand { get; } = new OK("Stand", false);
    public static State Sit { get; } = new OK("Sit", false);
    public static State Walk { get; } = new OK("Walk", true);
    public static State Run { get; } = new OK("Run", true);
    
    static TypedEnumSet<IState> ITypedEnum<IState>.Set => 
        TypedEnumSetBuilder.Create<IState>()
            .ReflectFromType(x => !ReferenceEquals(x, Undefined))
            .Build();

    private record Ok(
        string Value, 
        bool isMoved) : IState;
        
    private record Undef(): IState
    {
        public static Undef Instance { get; } = new ();
        public string Value => throw new Exception("Undefined");
    }
}

class Person 
{
   string Id { get; set; }
   IState State { get; set; } = IState.Undefined; 
}
```

При записи в базу 
```cs
    collection.InsertOne(new Person())
```

получим исклчение и тем самым объект с неопределенным состоянием не попадет в базу, 
часто бывает что при инициализации свойств часть из них бывает не инициализизоравна из-за невнимательности 

## Подключение 

- MongoDb

```cs
    BsonSerializer.RegisterSerializationProvider(new TypedEnumBsonSerializationProvider());
```
- System.Text.Json
```cs
    var options = new JsonSerializerOptions();
    options.Converters.Add(new TypedEnumJsonConverterFactory());
```
