using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EngineETL.Core.Domain.DTO;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EngineETL.Tools.Parsers
{
    public class InputToCityDTO
    {
        public IList<CityDTO> ParseToList(object data, TemplateDTO expectedFormat)
        {
            var mapJsonResultProperties = new Dictionary<string, string>() {
                { expectedFormat.CityPropertyName,"City"  },
                { expectedFormat.CityPropertyHabitants,"Habitants"},
                { expectedFormat.PropertyNeighborhood,"Neighborhoods"},
                { expectedFormat.NeighborhoodPropertyName, "Name"  },
                { expectedFormat.NeighborhoodPropertyHabitants, "Habitants"}
            };

            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            JObject parsedObject = JObject.Parse(json);

            var result = parsedObject.Descendants().OfType<JProperty>()
                .Select(p => new KeyValuePair<string, object>(p.Path, p.Value.Type == JTokenType.Array || p.Value.Type == JTokenType.Object ? null : p.Value));

            List<object> listObjects = GroupObjectProperties(expectedFormat.PropertyCity, result);

            return FillCities(expectedFormat, mapJsonResultProperties, listObjects);
            
        }

        private IList<CityDTO> FillCities(TemplateDTO expectedFormat, Dictionary<string, string> mapJsonResultProperties, List<object> listObjects)
        {
            var cities = new List<CityDTO>();

            foreach (var properties in listObjects)
            {
                var expectedProperties = (IDictionary<string, object>)TakeExpectedProperties(properties, mapJsonResultProperties);

                var dto = new CityDTO();
                dto.City = expectedProperties.FirstOrDefault(x => x.Key == expectedFormat.CityPropertyName).Value?.ToString(); // TO-DO: Tratar esses castings
                dto.Habitants = int.Parse(expectedProperties.FirstOrDefault(x => x.Key == expectedFormat.CityPropertyHabitants).Value?.ToString()); // TO-DO: Tratar esses castings

                var neighborhoods = expectedProperties.Where(x => x.Key != expectedFormat.CityPropertyName && x.Key != expectedFormat.CityPropertyHabitants);
                dto.Neighborhoods = FillNeighborhoods(neighborhoods, expectedFormat, mapJsonResultProperties);

                cities.Add(dto);
            }

            return cities;
        }

        private ICollection<NeighborhoodDTO> FillNeighborhoods(IEnumerable<KeyValuePair<string, object>> neighborhoods, TemplateDTO expectedFormat, Dictionary<string, string> mapJsonResultProperties)
        {
            var objList = GroupObjectProperties(expectedFormat.NeighborhoodPropertyName, neighborhoods);
            var list = new List<NeighborhoodDTO>();

            foreach (IDictionary<string, object> properties in objList)
            {
                var dto = new NeighborhoodDTO();
                dto.Name = properties[expectedFormat.NeighborhoodPropertyName]?.ToString();
                dto.Habitants = int.Parse(properties[expectedFormat.NeighborhoodPropertyHabitants]?.ToString());
                list.Add(dto);
            }
            return list;
        }

        private object TakeExpectedProperties(object properties, Dictionary<string, string> mapJsonResultProperties)
        {
            var mapResultProperties = new Dictionary<string, object>();
            var propertiesCopy = new Dictionary<string, object>();

            foreach (var resultProperty in mapJsonResultProperties)
            {
                var key = TakeTwoLastOrDefault(resultProperty.Key.Split("."));
                var value = resultProperty.Value;
                mapResultProperties.Add(key, value);
            }

            foreach (var objectProperty in properties as IDictionary<string, object>)
            {
                var key = TakeTwoLastOrDefault(objectProperty.Key.Split("."));
                var value = objectProperty.Value;
                propertiesCopy.Add(key, value);
            }

            var expectedProperties = new Dictionary<string, object>();

            foreach (var expectedProperty in propertiesCopy)
            {
                var property = mapResultProperties.FirstOrDefault(x => x.Key == RemoveArrayIndexes(expectedProperty.Key));

                if (property.Value != null)
                {
                    expectedProperties.Add(expectedProperty.Key, expectedProperty.Value);
                }
            }

            return expectedProperties;
        }


        private string TakeTwoLastOrDefault(string[] splitedPath)
        {
            var greatherThanTwo = splitedPath.Count() > 2;
            var joinedPath = string.Empty;

            if (!greatherThanTwo)
                return splitedPath.Join(".");

            return splitedPath.TakeLast(2)?.Join(".");
        }

        private List<object> GroupObjectProperties(string expectedFormat, IEnumerable<KeyValuePair<string, object>> result)
        {
            IEnumerable<KeyValuePair<string, object>> rootDocumentNodes = GetFromRootElement(expectedFormat, result);

            var pathValue = rootDocumentNodes.FirstOrDefault().Key;
            var arrayPathIndex = FindArrayIndexInPath(expectedFormat, pathValue);

            var currentObjectIndex = 0;
            var currentPropertyIndex = 0;

            var listObjects = new List<object>();
            var obj = new Dictionary<string, object>();

            foreach (var property in rootDocumentNodes)
            {
                if (arrayPathIndex != 0)
                {
                    currentPropertyIndex = int.Parse(property.Key.Substring(arrayPathIndex, 1));
                }

                string key = RemoveArrayIndexes(property.Key);

                if (currentObjectIndex == currentPropertyIndex)
                {
                    obj.Add(key, property.Value);
                }
                else
                {
                    listObjects.Add(Copy(obj));
                    obj = new Dictionary<string, object>();
                    currentObjectIndex++;
                    obj.Add(key, property.Value);
                }
            }

            if (obj.Keys?.Count > 0)
                listObjects.Add(obj);

            return listObjects;
        }

        private IEnumerable<KeyValuePair<string, object>> GetFromRootElement(string expectedFormat, IEnumerable<KeyValuePair<string, object>> result)
        {
            var rootDocumentNodes = result.Where(x => x.Value != null);
            var rootElement = rootDocumentNodes.FirstOrDefault(x => IsObjectRoot(x, expectedFormat));
            var rootIndex = rootDocumentNodes.IndexOf(rootElement);
            return rootDocumentNodes.Skip(rootIndex);
        }

        private bool IsObjectRoot(KeyValuePair<string, object> item, string expectedPath)
        {
            string key = RemoveArrayIndexes(item.Key);

            if (item.Value == null) return false;

            var isExpected = key.Contains(expectedPath);

            return isExpected;
        }

        private object Copy(Dictionary<string, object> obj)
        {
            return obj;
        }

        private int FindArrayIndexInPath(string expectedPath, string path)
        {
            var index = 0;
            if (!string.IsNullOrEmpty(path)) { index = path.IndexOf("[") + 1; }
            return index;
        }

        private string RemoveArrayIndexes(string path, bool removeAll = false)
        {
            var ocurrence = removeAll ? path.Length : 1;

            Regex rgx = new Regex(@"\[.*?\]");
            return rgx.Replace(path, "", ocurrence);
        }
    }
}
