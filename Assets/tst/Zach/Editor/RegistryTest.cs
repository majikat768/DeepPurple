using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class RegistryTest {

	[Test]
	public void GetOrDefaultTest() {
        Registry.registry.Clear();
        Assert.AreEqual(Registry.GetOrDefault<int>("test", -1), -1);
        Assert.AreNotEqual(Registry.GetOrDefault<int>("test", 0), 1);
        Registry.registry["test"] = 5;
        Assert.AreEqual(Registry.GetOrDefault<int>("test", 0), 5);
    }
}
