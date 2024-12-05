using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour
{
    [SerializeField] private List<Tag> _tags;

    public List<Tag> All => _tags;

    public bool HasTag(Tag tag)
    {
        return _tags.Contains(tag);
    }
    public bool HasTag(string tagName)
    {
        return _tags.Exists(tag => tag.Name == tagName);
    }
}
