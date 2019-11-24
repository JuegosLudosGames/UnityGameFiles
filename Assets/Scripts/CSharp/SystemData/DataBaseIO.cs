using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
using JLG.gift.cSharp.background;
using System.IO;
using JLG.gift.cSharp.background.scene;

namespace JLG.gift.cSharp.SystemData {
	public class DataBaseIO {

		//global
		byte saveNum;
		string globalPath;

		public DataBaseIO(byte saveNum) {
			Debug.Log("Starting save data");
			globalPath = Path.Combine(Application.persistentDataPath, "save" + saveNum);
			this.saveNum = saveNum;
			SqlSetupConnection(globalPath);
			Debug.Log("save data Path: " + globalPath);
		}

		public static void deleteSave(byte savenum) {
			Debug.Log("[Save Manager] Attempting to delete save number " + savenum);
			if (doesSaveExist(savenum)) {
				//File.Delete(Path.Combine(Application.persistentDataPath, "save" + saveNum + ".json"));
				Debug.Log("[Save Manager] deleting");
				Directory.Delete(Path.Combine(Application.persistentDataPath, "save" + savenum), true);
				Debug.Log("[Save Manager] Delete Successful");
			}
		}

		public static bool doesSaveExist(byte saveNum) {
			//string path = Path.Combine(Application.persistentDataPath, "save" + saveNum);
			return Directory.Exists(Path.Combine(Application.persistentDataPath, "save" + saveNum));
		}

		public static void createDirectorSchema(byte saveNum) {
			Debug.Log("[SaveManager] Checking for Directory");
			if (!doesSaveExist(saveNum)) {
				Debug.Log("[SaveManager] Directory not found, creating");
				Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "save" + saveNum));
				Debug.Log("[SaveManager] Directory created for save " + saveNum.ToString());
			}
		}

		//sql side
		string dbPath;

		public bool doesSqlDbExist() {
			return File.Exists(Path.Combine(globalPath, "data.db"));
		}

		public void SqlSetupConnection(string path) {
			Debug.Log("[Save Manager] [Sql] Preparing for SQLite Connection");
			dbPath = "URI=file:" + Path.Combine(globalPath, "data.db");
			//createSchemas();
		}

		public void createSceneSchema(string scene) {
			Debug.Log("[Save Manager] [Sql] creating Schema for Scene table \"" + scene + "\"");
			Debug.Log("[Save Manager] [Sql] Checking for file");
			if (!doesSqlDbExist()) {
				Debug.Log("[Save Manager] [Sql] File not found, creating");
				createDirectorSchema(saveNum);
				SqliteConnection.CreateFile(Path.Combine(globalPath, "data.db"));
				Debug.Log("[Save Manager] [Sql] File created");
			}
			using (var conn = new SqliteConnection(dbPath)) {
				conn.Open();
				Debug.Log("[Save Manager] [Sql] Connection opened");
				using (var cmd = conn.CreateCommand()) {

					cmd.CommandType = CommandType.Text;
					cmd.CommandText = "CREATE TABLE IF NOT EXISTS '" + scene + "' ( " +
									  " 'id' INTEGER PRIMARY KEY, " +
									  " 'state' INTEGER " +
									  " );";

					Debug.Log("[Save Manager] [Sql] Executing command");
					var result = cmd.ExecuteNonQuery();
					Debug.Log("[Save Manager] [Sql] Completed with result: " + result.ToString());
				}
			}
			Debug.Log("[Save Manager] [Sql] Schema Created");
		}

		IEnumerable createSceneSchemaTask(string scene) {
			Debug.Log("[Save Manager] [Sql] creating Schema for Scene table \"" + scene + "\"");
			Debug.Log("[Save Manager] [Sql] Checking for file");
			if (!doesSqlDbExist()) {
				Debug.Log("[Save Manager] [Sql] File not found, creating");
				createDirectorSchema(saveNum);
				yield return null;
				SqliteConnection.CreateFile(Path.Combine(globalPath, "data.db"));
				Debug.Log("[Save Manager] [Sql] File created");
			}
			yield return null;
			using (var conn = new SqliteConnection(dbPath)) {
				conn.Open();
				Debug.Log("[Save Manager] [Sql] Connection opened");
				yield return null;
				using (var cmd = conn.CreateCommand()) {
					yield return null;
					cmd.CommandType = CommandType.Text;
					cmd.CommandText = "CREATE TABLE IF NOT EXISTS '" + scene + "' ( " +
									  " 'id' INTEGER PRIMARY KEY, " +
									  " 'state' INTEGER " +
									  " );";

					Debug.Log("[Save Manager] [Sql] Executing command");
					var result = cmd.ExecuteNonQuery();
					Debug.Log("[Save Manager] [Sql] Completed with result: " + result.ToString());
					yield return null;
				}
				yield return null;
			}
			Debug.Log("[Save Manager] [Sql] Schema Created");
		}

		public SceneObjectData[] readSceneTable(string scene) {
			List<SceneObjectData> data;
			Debug.Log("[Save Manager] [Sql] reading from database for scene table \"" + scene + "\"");
			Debug.Log("[Save Manager] [Sql] creating Schema for table");
			createSceneSchema(scene);
			using (var conn = new SqliteConnection(dbPath)) {
				conn.Open();
				Debug.Log("[Save Manager] [Sql] Connection Opened");
				using (var cmd = conn.CreateCommand()) {
					cmd.CommandType = CommandType.Text;

					cmd.CommandText = "SELECT * FROM '" + scene + "';";

					Debug.Log("[Save Manager] [Sql] Executing command");
					var reader = cmd.ExecuteReader();
					Debug.Log("[Save Manager] [Sql] command executed");

					data = new List<SceneObjectData>();

					Debug.Log("[Save Manager] [Sql] interpreting data");
					while (reader.Read()) {
						var id = reader.GetInt32(0);
						var state = reader.GetInt32(1);
						data.Add(new SceneObjectData((byte)state, id));
					}
					Debug.Log("[Save Manager] [Sql] interpreting compelete with result of " + data.Count + "items read");
				}
			}
			return data.ToArray();
		}

		public void readSceneTableAsync(string scene, Action<SceneObjectData[]> act) {
			AsyncHandler.instance.startAsyncTask(readSceneTableTask(scene, act));
		}

		IEnumerator readSceneTableTask(string scene, Action<SceneObjectData[]> act) {
			List<SceneObjectData> data;
			Debug.Log("[Save Manager] [Sql] reading from database for scene table \"" + scene + "\"");
			Debug.Log("[Save Manager] [Sql] creating Schema for table");
			//createSceneSchema(scene);
			//while (createSceneSchemaTask(scene).MoveNext()) {
			//	yield return null;
			//}
			foreach (var a in createSceneSchemaTask(scene)) {
				yield return null;
			}
			using (var conn = new SqliteConnection(dbPath)) {
				conn.Open();
				Debug.Log("[Save Manager] [Sql] Connection Opened");
				yield return null;
				using (var cmd = conn.CreateCommand()) {
					yield return null;
					cmd.CommandType = CommandType.Text;

					cmd.CommandText = "SELECT * FROM '" + scene + "';";

					Debug.Log("[Save Manager] [Sql] Executing command");
					var reader = cmd.ExecuteReader();
					Debug.Log("[Save Manager] [Sql] command executed");
					yield return null;

					data = new List<SceneObjectData>();

					Debug.Log("[Save Manager] [Sql] interpreting data");
					while (reader.Read()) {
						var id = reader.GetInt32(0);
						var state = reader.GetInt32(1);
						data.Add(new SceneObjectData((byte)state, id));
						yield return null;
					}
					Debug.Log("[Save Manager] [Sql] interpreting compelete with result of " + data.Count + "items read");
				}
			}
			//return data.ToArray();
			act(data.ToArray());
		}

		public void updateSceneTable(string scene, SceneObjectData[] data) {
			Debug.Log("[Save Manager] [Sql] updating database for scene table \"" + scene + "\"");
			Debug.Log("[Save Manager] [Sql] creating Schema for table");
			createSceneSchema(scene);
			using (var conn = new SqliteConnection(dbPath)) {
				conn.Open();
				Debug.Log("[Save Manager] [Sql] Connection Opened");
				Debug.Log("[Save Manager] [Sql] inputing data for: " + data.Length + " objects");
				for (int x = 0; x < data.Length; x++) {
					Debug.Log("[Save Manager] [Sql] updating for object " + x + " in list");
					using (var cmd = conn.CreateCommand()) {
						cmd.CommandType = CommandType.Text;

						cmd.CommandText = "INSERT INTO '" + scene + "' (id, state) VALUES (@Id, @State) " +
											" ON DUPLICATE KEY UPDATE state=@States; ";

						SceneObjectData d = data[x];

						cmd.Parameters.Add(new SqliteParameter {
							ParameterName = "Id",
							Value = d.objectId
						});

						cmd.Parameters.Add(new SqliteParameter {
							ParameterName = "State",
							Value = d.state
						});

						cmd.Parameters.Add(new SqliteParameter {
							ParameterName = "States",
							Value = d.state
						});

						Debug.Log("[Save Manager] [Sql] Executing command");
						var result = cmd.ExecuteNonQuery();
						Debug.Log("[Save Manager] [Sql] Completed with result: " + result.ToString());
					}
				}
			}
			Debug.Log("[Save Manager] [Sql] Table updated");
		}

		public void updateSceneTableAsync(string scene, SceneObjectData[] data, Action onComplete) {
			AsyncHandler.instance.startAsyncTask(updateSceneTableTask(scene, data, onComplete));
		}

		IEnumerator updateSceneTableTask(string scene, SceneObjectData[] data, Action onComplete) {
			Debug.Log("[Save Manager] [Sql] updating database for scene table \"" + scene + "\"");
			Debug.Log("[Save Manager] [Sql] creating Schema for table");
			//while (createSceneSchemaTask(scene).MoveNext()) {
			//	yield return null;
			//}
			foreach (var a in createSceneSchemaTask(scene)) {
				yield return null;
			}
			using (var conn = new SqliteConnection(dbPath)) {
				conn.Open();
				Debug.Log("[Save Manager] [Sql] Connection Opened");
				yield return null;
				Debug.Log("[Save Manager] [Sql] inputing data for: " + data.Length + " objects");
				for (int x = 0; x < data.Length; x++) {
					Debug.Log("[Save Manager] [Sql] updating for object " + x + " in list");
					using (var cmd = conn.CreateCommand()) {
						yield return null;
						cmd.CommandType = CommandType.Text;

						cmd.CommandText = "INSERT INTO '" + scene + "' (id, state) VALUES (@Id, @State) ON CONFLICT(id) DO UPDATE SET state = @States; ";

						SceneObjectData d = data[x];

						cmd.Parameters.Add(new SqliteParameter {
							ParameterName = "Id",
							Value = d.objectId
						});
						yield return null;
						cmd.Parameters.Add(new SqliteParameter {
							ParameterName = "State",
							Value = d.state
						});
						yield return null;
						cmd.Parameters.Add(new SqliteParameter {
							ParameterName = "States",
							Value = d.state
						});
						yield return null;
						Debug.Log("[Save Manager] [Sql] Executing command");
						var result = cmd.ExecuteNonQuery();
						Debug.Log("[Save Manager] [Sql] Completed with result: " + result.ToString());
						yield return null;
					}
					yield return null;
				}
			}
			Debug.Log("[Save Manager] [Sql] Table updated");
			onComplete();
		}

		/////////////
		//json side//
		/////////////
		public static readonly string PlayerJsonPath = "playerData.json";
		public static readonly string SettingJsonPath = "settings.json";

		public void JsonSaveDataAsync(Action onFinish, string file, object obj) {
			Debug.Log("[Save Manager] [Json] Saving " + file + " with Async");
			//temp_saveNum = saveNum;
			this.act = onFinish;
			AsyncHandler.instance.startAsyncTask(saveDataCo(file, obj));
		}

		void JsonSaveDirect(string file, object obj) {
			Debug.Log("[Save Manager] [Json] Saving " + file + " without Async");
			createDirectorSchema(saveNum);
			string path = Path.Combine(globalPath, file);
			string rawJson = JsonUtility.ToJson(obj);
			using (StreamWriter writer = File.CreateText(path)) {
				writer.Write(rawJson);
			}
			Debug.Log("[Save Manager] [Json] Save Complete");
			if (!(act is null)) {
				act.Invoke();
			}
		}

		//byte temp_saveNum;
		Action act;

		IEnumerator saveDataCo(string file, object obj) {
			Debug.Log("[Save Manager] [Json] started Async Task");
			createDirectorSchema(saveNum);
			string path = Path.Combine(globalPath, file);
			string rawJson = JsonUtility.ToJson(obj);
			yield return null;
			using (StreamWriter writer = File.CreateText(path)) {
				writer.Write(rawJson);
			}
			yield return null;
			Debug.Log("[Save Manager] [Json] Save Complete");
			if (!(act is null))
				act.Invoke();
		}

		public void LoadDataAsync<T>(Action<T> onLoadComplete, string file) {
			Debug.Log("[Save Manager] [Json] Loading " + file + " with Async");
			//this.saveNum = saveNum;
			//this.onLoadComplete = onLoadComplete;
			AsyncHandler.instance.startAsyncTask(loadDataCo<T>(file, onLoadComplete));
		}

		IEnumerator loadDataCo<T>(string file, Action<T> onLoadComplete) {
			Debug.Log("[Save Manager] [Json] started Async Task");
			string path = Path.Combine(globalPath, file);

			if (!doesJsonSaveExist(file)) {
				Debug.LogError("[Save Manager] [Json] [ERROR] file " + file + " failed to load, FileNotFoundException");
				throw new FileNotFoundException(file);
			}

			yield return null;

			T loadedSave;
			using (StreamReader reader = File.OpenText(path)) {
				string rawJson = reader.ReadToEnd();
				yield return null;
				loadedSave = JsonUtility.FromJson<T>(rawJson);
			}
			yield return null;
			Debug.Log("[Save Manager] [Json] load Complete");
			onLoadComplete(loadedSave);
		}


		public bool doesJsonSaveExist(string file) {
			string path = Path.Combine(globalPath, file);
			return File.Exists(path);
		}

	}
}