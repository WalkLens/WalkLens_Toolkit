// Copyright (c) Microsoft Corporation. 
// Licensed under the MIT License.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using MRTK.Tutorials.AzureCloudServices.Scripts.Domain;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace MRTK.Tutorials.AzureCloudServices.Scripts.Managers
{
    public class DataManagerCtrl : MonoBehaviour
    {
        public bool IsReady { get; private set; }
        public GameObject NoticeUI;

        [Header("Base Settings")]
        [SerializeField]
        private string connectionString = default;

        [Header("Table Settings")]
        [SerializeField]
        private string membersTableName = "Members";
        [SerializeField]
        private bool tryCreateTableOnStart = true;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onDataManagerReady = default;
        [SerializeField]
        private UnityEvent onDataManagerInitFailed = default;

        private CloudStorageAccount storageAccount;
        private CloudTableClient cloudTableClient;
        private CloudTable membersTable;

        private int partitionKeyCounter;
        private int rowKeyCounter;

        // New field to store all users
        private List<UserEntity> allUsersList;

        private async void Awake()
        {
            partitionKeyCounter = PlayerPrefs.GetInt("PartitionKeyCounter", 0);
            rowKeyCounter = PlayerPrefs.GetInt("RowKeyCounter", 0);

            storageAccount = CloudStorageAccount.Parse(connectionString);
            cloudTableClient = storageAccount.CreateCloudTableClient();
            membersTable = cloudTableClient.GetTableReference(membersTableName);

            if (tryCreateTableOnStart)
            {
                try
                {
                    if (await membersTable.CreateIfNotExistsAsync())
                    {
                        Debug.Log($"Created table {membersTableName}.");
                    }
                }
                catch (StorageException ex)
                {
                    Debug.LogError("Failed to connect with Azure Storage.\nPlease make sure you have the correct connection string.");
                    Debug.LogException(ex);
                    onDataManagerInitFailed?.Invoke();
                }
            }

            IsReady = true;
            onDataManagerReady?.Invoke();

            await LoadAndStoreAllUsers(); // Modified to load and store all users
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetInt("PartitionKeyCounter", partitionKeyCounter);
            PlayerPrefs.SetInt("RowKeyCounter", rowKeyCounter);
            PlayerPrefs.Save();
        }

        public async Task<bool> SaveUser(string partitionKey, string name, string password, string university, string major, [CanBeNull] string selfIntroduction, 
            string[] group, string generation, string[] project, [CanBeNull] string job, [CanBeNull] string companyName, [CanBeNull] string duty, 
            string[] skill, string[] interest)
        {

            string groups = GetStringValue(group);
            string projects = GetStringValue(project);
            string skills = GetStringValue(skill);
            string interests = GetStringValue(interest);

            UserEntity userEntity = new UserEntity(partitionKey, name, password, university, major, selfIntroduction, groups, generation, projects, job, 
                companyName, duty, skills, interests);
            TableOperation insertOperation = TableOperation.Insert(userEntity);

            NoticePartitionNumForUserLogIN();
            var result = await membersTable.ExecuteAsync(insertOperation);

            return result.HttpStatusCode == (int)HttpStatusCode.NoContent;
        }

        private string GetStringValue(string[] values)
        {
            // 배열 요소들을 콤마로 구분된 하나의 문자열로 결합합니다.
            return string.Join(",", values);
        }

        private string[] GetStringArray(string value)
        {
            return value.Split(',');
        }
        public async Task<UserEntity> LoadUser(string rowKey)
        {
            string partitionKey = ""; // 기본적으로 모든 파티션을 검색하려면 빈 문자열 사용
            TableQuery<UserEntity> query = new TableQuery<UserEntity>()
                .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey));

            var result = await membersTable.ExecuteQuerySegmentedAsync(query, null);
            return result.Results.FirstOrDefault();
        }

        public async Task<List<UserEntity>> LoadAllUsers()
        {
            TableQuery<UserEntity> query = new TableQuery<UserEntity>();
            List<UserEntity> results = new List<UserEntity>();
            TableContinuationToken token = null;

            do{
                var queryResult = await membersTable.ExecuteQuerySegmentedAsync(query, token);
                results.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            }while(token!=null);

            return results;
        }

        // New method to load and store all users
        private async Task LoadAndStoreAllUsers()
        {
            allUsersList = await LoadAllUsers();
            PrintAllUsers();
        }

        public void PrintAllUsers()
        {
            if (allUsersList == null) return;
            
            foreach (var user in allUsersList)
            {
                Debug.Log($"PartitionKey: {user.PartitionKey}, Name: {user.Name}, Job: {user.Password}, Major: {user.Major}," +
                          $"Group Info: {user.Group} Generation {user.Password}, Projects: {user.Project},");
            }
        }

        public class UserEntity : TableEntity
        {
            public UserEntity(string partitionKey, string name, string password, string university, string major, [CanBeNull] string selfIntroduction, 
                string group, string generation, string project, [CanBeNull] string job, [CanBeNull] string companyName, [CanBeNull] string duty, 
                string skill, string interest)
            {
                this.PartitionKey = partitionKey; // TODO : User의 입력으로 변경
                this.RowKey = partitionKey;
                
                this.Name = name;
                this.Password = password;
                this.University = university;
                this.Major = major;
                this.SelfIntroduction = selfIntroduction;

                this.Group = group;
                this.Generation = generation;
                this.Project = project;
                
                this.Job = job;
                this.CompanyName = companyName;
                this.Duty = duty;

                this.Skill = skill;
                
                this.Interest = interest;
            }

            public UserEntity() { }

            public string Name { get; set; }            
            public string Password { get; set; }
            public string University { get; set; }
            public string Major { get; set; }
            public string SelfIntroduction { get; set; }
            public string Group { get; set; }
            public string Generation { get; set; }
            public string Project { get; set; }
            
            public string Job { get; set; }
            public string CompanyName { get; set; }
            public string Duty { get; set; }
            
            public string Skill { get; set; }
            public string Interest { get; set; }
        }

        public void NoticePartitionNumForUserLogIN()
        {
            NoticeUI.GetComponent<TextMeshProUGUI>().text = (partitionKeyCounter).ToString();
        }
    }

    
}
