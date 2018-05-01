using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PodcastRadio.Core.Models.DTOs;

namespace PodcastRadio.Core.Helpers
{
    public static class ReflectionHelper
    {
        // To check if viewcontroller is of type XViewController<> or XTabBarViewController in case of iOS or activity is of type XActivity for Android
        public static bool IsSubclassOfRawGeneric(this Type toCheck, Type generic)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.GetTypeInfo().IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;

                if (generic == cur)
                    return true;

                toCheck = toCheck.GetTypeInfo().BaseType;
            }
            return false;
        }

        // To check if a viewcontroller or activity is associated with a viewmodel, and return all viewmodels types
        public static Type GetBaseGenericType(Type child)
        {
            if (child.GetTypeInfo().BaseType == null)
                return null;

            var generics = child.GetTypeInfo().BaseType.GenericTypeArguments;
            if (generics.Length == 0)
                return null;

            return generics[0];
        }

        public static Type ConvertNullToEmpty<Type>(Type model)
        {
            foreach (var propertyInfo in model.GetType().GetProperties())
            {
                if (propertyInfo.PropertyType == typeof(string))
                {
                    if (propertyInfo.GetValue(model, null) == null)
                    {
                        propertyInfo.SetValue(model, string.Empty, null);
                    }
                }
            }
            return model;
        }
    }
}

//https://stackoverflow.com/questions/12087606/how-to-get-base-classs-generic-type-parameter
//https://stackoverflow.com/questions/4592644/how-to-access-generic-property-without-knowing-the-closed-generic-type