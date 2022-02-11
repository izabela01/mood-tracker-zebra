using System;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using MoodTracker.Models;

namespace MoodTracker.Extensions
{
    public static class DataAttributeExtensions
    {
        public static T getAttributeForProperty<T>(this Type type, string propertyName) where T: Attribute
        {
            var propertyInfo = type.GetProperty(propertyName);
            if(propertyInfo == null) throw new MissingFieldException("Property not found", propertyName);
            return (T) propertyInfo.GetCustomAttribute(typeof(T));
        }
        
        public static T getAttributeForProperty<T>(this object instance, string propertyName) where T: Attribute
        {
            return instance.GetType().getAttributeForProperty<T>(propertyName);
        }
    }
}