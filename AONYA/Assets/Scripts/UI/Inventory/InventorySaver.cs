using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class InventorySaver : MonoBehaviour {
    [Header("Inventory")]
    [SerializeField] private Inventory inventory;

    private void OnEnable() {
        inventory.playerInventory.Clear();
        LoadScriptables();
    }

    private void OnDisable() {
        SaveScriptables();
    }

    public void ResetScriptables() {
        int i = 0;
        while (File.Exists(Application.persistentDataPath + string.Format("/{0}.inv", i))) {
            File.Delete(Application.persistentDataPath + string.Format("/{0}.inv", i));

            i++;
        }
    }

    public void SaveScriptables() {
        ResetScriptables();
        for (int i = 0; i < inventory.playerInventory.Count; i++) {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.inv", i));
            BinaryFormatter binary = new BinaryFormatter();

            var json = JsonUtility.ToJson(inventory.playerInventory[i]);
            binary.Serialize(file, json);

            file.Close();
        }
    }

    private void LoadScriptables() {
        int i = 0;

        // while there is a file in the data path loop
        while (File.Exists(Application.persistentDataPath + string.Format("/{0}.inv", i))) {
            // create a temp inventory item, that will then be overwritten--this is essentially creating a copy of the item 
            var temp = ScriptableObject.CreateInstance<InventoryItem>();

            FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.inv", i), FileMode.Open);

            // this allows us to take information fram a file and put it back onto a gameobject
            BinaryFormatter binary = new BinaryFormatter();

            // take the information from the file as a string, overwrite temp item
            JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), temp);
            file.Close();

            // add temp item to inventory
            inventory.playerInventory.Add(temp);
            i++;
        }
    }
}
