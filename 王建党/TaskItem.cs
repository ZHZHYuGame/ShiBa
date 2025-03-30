using UnityEngine;
using System.Collections.Generic;

public class TaskItem : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();
    public List<Quest> activeQuests = new List<Quest>();
    public List<Quest> completedQuests = new List<Quest>();

    private Dictionary<int, Quest> questDictionary = new Dictionary<int, Quest>();

    void Start()
    {
        LoadQuests();
    }

    public void LoadQuests()
    {
        // ���ļ������ݿ������������
    }

    public void AddQuest(Quest quest)
    {
        if (!questDictionary.ContainsKey(quest.questID))
        {
            questDictionary.Add(quest.questID, quest);
            if (quest.isRepeatable || !IsQuestCompleted(quest.questID))
            {
                activeQuests.Add(quest);
            }
        }
    }

    public void RemoveQuest(int questID)
    {
        if (questDictionary.ContainsKey(questID))
        {
            Quest quest = questDictionary[questID];
            activeQuests.Remove(quest);
            completedQuests.Add(quest);
        }
    }

    public bool IsQuestActive(int questID)
    {
        return activeQuests.Exists(q => q.questID == questID);
    }

    public bool IsQuestCompleted(int questID)
    {
        return completedQuests.Exists(q => q.questID == questID);
    }

    public void CompleteQuest(int questID)
    {
        if (IsQuestActive(questID))
        {
            RemoveQuest(questID);
            // ���Ž���
            GiveRewards(questID);
        }
    }

    public void GiveRewards(int questID)
    {
        if (questDictionary.ContainsKey(questID))
        {
            Quest quest = questDictionary[questID];
            foreach (Reward reward in quest.rewards)
            {
                //// ���ݽ������ͷ��Ž���
                //if (reward.rewardType == RewardType.Gold)
                //{
                //    PlayerStats.Gold += reward.amount;
                //}
                //else if (reward.rewardType == RewardType.Exp)
                //{
                //    PlayerStats.Exp += reward.amount;
                //}
                // ������������...
            }
        }
    }

    public void UpdateObjective(int questID, int objectiveIndex, int amount = 1)
    {
        if (IsQuestActive(questID))
        {
            Quest quest = activeQuests.Find(q => q.questID == questID);
            if (quest != null && objectiveIndex >= 0 && objectiveIndex < quest.objectives.Count)
            {
                quest.objectives[objectiveIndex].currentCount += amount;
                if (quest.objectives[objectiveIndex].currentCount >= quest.objectives[objectiveIndex].targetCount)
                {
                    quest.objectives[objectiveIndex].isCompleted = true;
                    // �������Ŀ���Ƿ����
                    bool allObjectivesCompleted = true;
                    foreach (QuestObjective obj in quest.objectives)
                    {
                        if (!obj.isCompleted)
                        {
                            allObjectivesCompleted = false;
                            break;
                        }
                    }
                    if (allObjectivesCompleted)
                    {
                        CompleteQuest(questID);
                    }
                }
            }
        }
    }
}