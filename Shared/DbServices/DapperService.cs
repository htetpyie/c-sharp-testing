﻿using Dapper;
using MySqlConnector;
using System.Data;

namespace Shared.DbServices;

public class DapperService
{
	private readonly string _connectionString;

	public DapperService(string connectionString)
	{
		_connectionString = connectionString;
	}

	public List<M> Query<M>(string query, object? param = null)
	{
		using IDbConnection db = new MySqlConnection(_connectionString);
		//if (param != null)
		//{
		//    var lst = db.Query<M>(query, param).ToList();
		//}
		//else
		//{
		//    var lst = db.Query<M>(query).ToList();
		//}
		var lst = db.Query<M>(query, param).ToList();
		return lst;
	}

	public M QueryFirstOrDefault<M>(string query, object? param = null)
	{
		using IDbConnection db = new MySqlConnection(_connectionString);
		//if (param != null)
		//{
		//    var lst = db.Query<M>(query, param).ToList();
		//}
		//else
		//{
		//    var lst = db.Query<M>(query).ToList();
		//}
		var item = db.Query<M>(query, param).FirstOrDefault();
		return item!;
	}

	public int Execute(string query, object? param = null)
	{
		using IDbConnection db = new MySqlConnection(_connectionString);
		var result = db.Execute(query, param);
		return result;
	}
}