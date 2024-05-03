using _Game.Scripts.SkillSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class TestYAML : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Database/SkillTree/skillTree");
        var input = new StringReader(textAsset.text);

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var order = deserializer.Deserialize<SkillTree>(input);

        foreach (SkillNode node in order.Roots)
        {
            Debug.Log(node.Id);
        }
    }

}
