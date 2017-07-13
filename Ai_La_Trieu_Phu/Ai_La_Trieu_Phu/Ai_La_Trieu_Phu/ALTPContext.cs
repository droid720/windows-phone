#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Database
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Data.Linq.Mapping;
using Microsoft.Phone.Data.Linq;


public class DebugWriter : TextWriter
{
    private const int DefaultBufferSize = 256;
    private System.Text.StringBuilder _buffer;

    public DebugWriter()
    {
        BufferSize = 256;
        _buffer = new System.Text.StringBuilder(BufferSize);
    }

    public int BufferSize
    {
        get;
        private set;
    }

    public override System.Text.Encoding Encoding
    {
        get { return System.Text.Encoding.UTF8; }
    }

    #region StreamWriter Overrides
    public override void Write(char value)
    {
        _buffer.Append(value);
        if (_buffer.Length >= BufferSize)
            Flush();
    }

    public override void WriteLine(string value)
    {
        Flush();

        using(var reader = new StringReader(value))
        {
            string line; 
            while( null != (line = reader.ReadLine()))
                System.Diagnostics.Debug.WriteLine(line);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            Flush();
    }

    public override void Flush()
    {
        if (_buffer.Length > 0)
        {
            System.Diagnostics.Debug.WriteLine(_buffer);
            _buffer.Clear();
        }
    }
    #endregion
}


	public partial class ALTPContext : System.Data.Linq.DataContext
	{
		
		public bool CreateIfNotExists()
		{
			bool created = false;
			if (!this.DatabaseExists())
			{
				string[] names = this.GetType().Assembly.GetManifestResourceNames();
				string name = names.Where(n => n.EndsWith(FileName)).FirstOrDefault();
				if (name != null)
				{
					using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
					{
						if (resourceStream != null)
						{
							using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
							{
								using (IsolatedStorageFileStream fileStream = new IsolatedStorageFileStream(FileName, FileMode.Create, myIsolatedStorage))
								{
									using (BinaryWriter writer = new BinaryWriter(fileStream))
									{
										long length = resourceStream.Length;
										byte[] buffer = new byte[32];
										int readCount = 0;
										using (BinaryReader reader = new BinaryReader(resourceStream))
										{
											// read file in chunks in order to reduce memory consumption and increase performance
											while (readCount < length)
											{
												int actual = reader.Read(buffer, 0, buffer.Length);
												readCount += actual;
												writer.Write(buffer, 0, actual);
											}
										}
									}
								}
							}
							created = true;
						}
						else
						{
							this.CreateDatabase();
							created = true;
						}
					}
				}
				else
				{
					this.CreateDatabase();
					created = true;
				}
			}
			return created;
		}
		
		public bool LogDebug
		{
			set
			{
				if (value)
				{
					this.Log = new DebugWriter();
				}
			}
		}
		
		public static string ConnectionString = "Data Source=isostore:/ALTP.sdf";

		public static string ConnectionStringReadOnly = "Data Source=appdata:/ALTP.sdf;File Mode=Read Only;";

		public static string FileName = "ALTP.sdf";

		public ALTPContext(string connectionString) : base(connectionString)
		{
			OnCreated();
		}
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertHighScore(HighScore instance);
    partial void UpdateHighScore(HighScore instance);
    partial void DeleteHighScore(HighScore instance);
    #endregion
		
		public System.Data.Linq.Table<HighScore> HighScores
		{
			get
			{
				return this.GetTable<HighScore>();
			}
		}
		
		public System.Data.Linq.Table<Question> Questions
		{
			get
			{
				return this.GetTable<Question>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute()]
	public partial class HighScore : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Name;
		
		private System.Nullable<int> _Score;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnScoreChanging(System.Nullable<int> value);
    partial void OnScoreChanged();
    #endregion
		
		public HighScore()
		{
			OnCreated();
		}

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id",AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL", IsPrimaryKey = true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(100)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Score", DbType="Int")]
		public System.Nullable<int> Score
		{
			get
			{
				return this._Score;
			}
			set
			{
				if ((this._Score != value))
				{
					this.OnScoreChanging(value);
					this.SendPropertyChanging();
					this._Score = value;
					this.SendPropertyChanged("Score");
					this.OnScoreChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Index(Name="UQ__Question__00000000000000BC", Columns="ID_QUESTION ASC", IsUnique=true)]
	[global::System.Data.Linq.Mapping.TableAttribute()]
	public partial class Question
	{
		
		private System.Nullable<double> _ID_QUESTION;
		
		private System.Nullable<double> _KIND_QUESTION;
		
		private string _QUESTION_CONTENT;
		
		private string _QUESTION_1;
		
		private string _QUESTION_2;
		
		private string _QUESTION_3;
		
		private string _QUESTION_4;
		
		private System.Nullable<double> _ANSWER_CONTENT;
		
		private System.Nullable<double> _QUESTION_NO;
		
		public Question()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_QUESTION", DbType="Float")]
		public System.Nullable<double> ID_QUESTION
		{
			get
			{
				return this._ID_QUESTION;
			}
			set
			{
				if ((this._ID_QUESTION != value))
				{
					this._ID_QUESTION = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_KIND_QUESTION", DbType="Float")]
		public System.Nullable<double> KIND_QUESTION
		{
			get
			{
				return this._KIND_QUESTION;
			}
			set
			{
				if ((this._KIND_QUESTION != value))
				{
					this._KIND_QUESTION = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_QUESTION_CONTENT", DbType="NText", UpdateCheck=UpdateCheck.Never)]
		public string QUESTION_CONTENT
		{
			get
			{
				return this._QUESTION_CONTENT;
			}
			set
			{
				if ((this._QUESTION_CONTENT != value))
				{
					this._QUESTION_CONTENT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_QUESTION_1", DbType="NText", UpdateCheck=UpdateCheck.Never)]
		public string QUESTION_1
		{
			get
			{
				return this._QUESTION_1;
			}
			set
			{
				if ((this._QUESTION_1 != value))
				{
					this._QUESTION_1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_QUESTION_2", DbType="NText", UpdateCheck=UpdateCheck.Never)]
		public string QUESTION_2
		{
			get
			{
				return this._QUESTION_2;
			}
			set
			{
				if ((this._QUESTION_2 != value))
				{
					this._QUESTION_2 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_QUESTION_3", DbType="NText", UpdateCheck=UpdateCheck.Never)]
		public string QUESTION_3
		{
			get
			{
				return this._QUESTION_3;
			}
			set
			{
				if ((this._QUESTION_3 != value))
				{
					this._QUESTION_3 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_QUESTION_4", DbType="NText", UpdateCheck=UpdateCheck.Never)]
		public string QUESTION_4
		{
			get
			{
				return this._QUESTION_4;
			}
			set
			{
				if ((this._QUESTION_4 != value))
				{
					this._QUESTION_4 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ANSWER_CONTENT", DbType="Float")]
		public System.Nullable<double> ANSWER_CONTENT
		{
			get
			{
				return this._ANSWER_CONTENT;
			}
			set
			{
				if ((this._ANSWER_CONTENT != value))
				{
					this._ANSWER_CONTENT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_QUESTION_NO", DbType="Float")]
		public System.Nullable<double> QUESTION_NO
		{
			get
			{
				return this._QUESTION_NO;
			}
			set
			{
				if ((this._QUESTION_NO != value))
				{
					this._QUESTION_NO = value;
				}
			}
		}
	}
}
#pragma warning restore 1591