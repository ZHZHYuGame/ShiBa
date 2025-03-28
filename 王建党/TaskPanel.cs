using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanel : MonoBehaviour
{
    public Text questLogText;
    public GameObject questHighlightPrefab;
    public Transform highlightParent;

    private QuestManager questManager;
    private List<GameObject> highlightObjects = new List<GameObject>();

    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        if (questManager == null)
        {
            Debug.LogError("QuestManager not found!");
            return;
        }

        UpdateQuestLog();
    }

    public void UpdateQuestLog()
    {
        if (questLogText == null)
        {
            Debug.LogError("Quest log text UI element not assigned!");
            return;
        }

        questLogText.text = "Active Quests:\n";
        foreach (Quest quest in questManager.activeQuests)
        {
            questLogText.text += $"- {quest.questName}\n";
            foreach (QuestObjective obj in quest.objectives)
            {
                questLogText.text += $"  - {obj.objectiveDescription}: {obj.currentCount}/{obj.targetCount}\n";
            }
        }
    }

    public void HighlightObjectivePosition(Vector3 position)
    {
        // ������еĸ�����ʾ
        ClearHighlights();

        // �����µĸ�������
        GameObject highlight = Instantiate(questHighlightPrefab, position, Quaternion.identity, highlightParent);
        highlightObjects.Add(highlight);
    }

    public void ClearHighlights()
    {
        foreach (GameObject highlight in highlightObjects)
        {
            Destroy(highlight);
        }
        highlightObjects.Clear();
    }
}