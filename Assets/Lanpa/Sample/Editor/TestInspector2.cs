using UnityEditor;

namespace Lanpa
{
    [CustomEditor(typeof(ReferenceCollector))]
    [CanEditMultipleObjects]
    public class TestInspector2 : LInspectorCreator<ReferenceCollector>
    {

    }
}