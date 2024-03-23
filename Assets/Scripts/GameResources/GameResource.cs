using UnityEngine;

// Ten atrybut pozwala nam tworzyæ nowe instancje GameResource za pomoc¹ edytora Unity.
[CreateAssetMenu(fileName = "NewGameResource", menuName = "Game Resources/GameResource")]
public class GameResource : ScriptableObject
{
    // Nazwa GameResource. Mo¿esz dostosowaæ to, aby zawiera³o bardziej szczegó³owe informacje.
    public string resourceName;

    // Referencja do Prefabu. Pozwala to na skojarzenie okreœlonego prefabrykatu z tym GameResource.
    public GameObject prefab;
}
