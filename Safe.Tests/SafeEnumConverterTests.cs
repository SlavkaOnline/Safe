using System.Text.Json;
using Safe.Json;
using Safe.Set;

namespace Safe.Tests;

public class SafeEnumConverterTests
{
    abstract class SafeEnum : ISafeEnum<SafeEnum>
    {
        public abstract string Value { get; }
        
        
        public static SafeEnum Undefined => Err.Instance;

        public static SafeEnum One { get; } = new Ok("1");
        public static SafeEnum Two  { get; } = new Ok("2");
        public static SafeEnum Three  { get; } = new Ok("3");
        
        public static SafeEnumSet<SafeEnum> Set => SafeEnumSetBuilder.Create<SafeEnum>()
            .ReflectFromType(x => !ReferenceEquals(x, Undefined))
            .Build();
        
        private class Ok(string value) : SafeEnum
        {
            public override string Value { get; } = value;
        }
        
        private class Err(): SafeEnum
        {
            public static Err Instance { get; } = new ();
            public override string Value => throw new Exception("tttt");
        }
    }
    
    interface SafeEnumInt : ISafeEnum<SafeEnumInt>
    {
        
        public static SafeEnumInt Undefined { get; } = Err.Instance;

        public static SafeEnumInt One { get; } = new Ok("1");
        public static SafeEnumInt Two  { get; } = new Ok("2");
        public static SafeEnumInt Three  { get; } = new Ok("3");
        
        static SafeEnumSet<SafeEnumInt> ISafeEnum<SafeEnumInt>.Set => 
            SafeEnumSetBuilder.Create<SafeEnumInt>()
            .ReflectFromType(x => !ReferenceEquals(x, Undefined))
            .Build();
        
        private struct Ok(string value) : SafeEnumInt
        {
            public string Value { get; } = value;
        }
        
        private struct Err(): SafeEnumInt
        {
            public static Err Instance { get; } = new ();
            public string Value => throw new Exception("tttt");
        }
    }
    
    
    
    record Model(SafeEnum EValue);
    
    record ModelInt(SafeEnumInt EValue);

    
    [Fact]
    public void Deserialize()
    {
        var str = """
                  {
                    "EValue": "2"
                  }
                  """;

        var options = new JsonSerializerOptions();
        options.Converters.Add(new SafeEnumJsonConverterFactory());

        var model = JsonSerializer.Deserialize<ModelInt>(str, options);

        Assert.Equal("2", model!.EValue.Value);
    }
    
    [Fact]
    public void Serialize()
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new SafeEnumJsonConverterFactory());

        var json = JsonSerializer.Serialize(new Model(SafeEnum.Three), options);

        Assert.Equal("""{"EValue":"3"}""", json);
    }
    
    // [Fact]
    // public void SerializeDefault()
    // {
    //     var options = new JsonSerializerOptions();
    //     options.Converters.Add(new SafeEnumJsonConverterFactory());
    //
    //     var json = JsonSerializer.Serialize(new Model(ISafeEnum<SafeEnum>.Default), options);
    //
    //     Assert.Equal("""{"EValue":"3"}""", json);
    // }
}