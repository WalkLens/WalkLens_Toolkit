// Copyright (c) Microsoft Corporation. 
// Licensed under the MIT License.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using MRTK.Tutorials.AzureCloudServices.Scripts.Domain;
using UnityEngine;
using UnityEngine.Events;

namespace MRTK.Tutorials.AzureCloudServices.Scripts.Managers
{
    public class DataManagerCtrl : MonoBehaviour
    {
        public bool IsReady { get; private set; }

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

        public async Task<bool> SaveUser(string name, string job, string hobby)
        {
            string partitionKey = (partitionKeyCounter++).ToString();
            string rowKey = (rowKeyCounter++).ToString();

            UserEntity userEntity = new UserEntity(partitionKey, rowKey, name, job, hobby);
            TableOperation insertOperation = TableOperation.Insert(userEntity);
            var result = await membersTable.ExecuteAsync(insertOperation);

            return result.HttpStatusCode == (int)HttpStatusCode.NoContent;
        }

        public async Task<UserEntity> LoadUser(string rowKey)
        {
            string partitionKey = ""; 
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
                Debug.Log($"PartitionKey: {user.PartitionKey}, RowKey: {user.RowKey}, Name: {user.Name}, Job: {user.Job}, Hobby: {user.Hobby}");
            }
        }

        public class UserEntity : TableEntity
        {
            public UserEntity(string partitionKey, string rowKey, string name, string job, string hobby)
            {
                this.PartitionKey = partitionKey;
                this.RowKey = rowKey;
                this.Name = name;
                this.Job = job;
                this.Hobby = hobby;
            }

            public UserEntity() { }

            public string Name { get; set; }
            public string Job { get; set; }
            public string Hobby { get; set; }
        }
    }
}
