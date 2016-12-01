using ICICore.Mvc.Web;
using ICICore.Mvc.Web.Atributos;
using ICICore.Web.Configuracoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Templating;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace ICICore.Web.Templates.ViewGenerator
{
	/// <summary>
	/// Funções compartilhadas pelos templates de view.
	/// </summary>
	public static class ViewGerador
	{
		/// <summary>
		/// Construtor.
		/// </summary>
		static ViewGerador()
		{
			ConfigCopy = new Configuracao();
		}

		/// <summary>
		/// Configurações do sistema.
		/// </summary>
		public static Configuracao ConfigCopy;

		/// <summary>
		/// Necessário para todar teste
		/// </summary>
		public static bool ModoTeste = false;

		/// <summary>
		/// Referência da classe teste que está chamando a classe de funções.
		/// </summary>
		public static Type Referencia;

		/// <summary>
		/// Referência do modelo do gerador de viewmodel.
		/// </summary>
		public static ViewGeneratorTemplateModel ModelRef { get; set; }

		/// <summary>
		/// Obter uma lista com os nomes das chaves primárias.
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<string> GetPrimaryKeyNames()
		{
			foreach (var item in GetPrimaryKeys())
			{
				yield return item.Name;
			}
		}

		/// <summary>
		/// Obter do atributo DataType, que contém informações adicionais sobre os dados válidos para campo texto.
		/// </summary>
		/// <param name="property">Ligação para propriedade reflexiva do modelo</param>
		/// <returns>string</returns>
		public static DataTypeAttribute GetDataType(PropertyInfo property)
		{
			var dataType = property.GetCustomAttribute<DataTypeAttribute>();
			if (dataType == null)
			{
				if (property.PropertyType.IsAssignableFrom(typeof(Boolean)))
				{
					return new DataTypeAttribute(DataTypes.CheckBox);
				}
				else if (property.PropertyType.IsAssignableFrom(typeof(DateTime)))
				{
					return new DataTypeAttribute(DataTypes.DateTime);
				}
				else if (property.PropertyType.IsAssignableFrom(typeof(TimeSpan)))
				{
					return new DataTypeAttribute(DataTypes.Time);
				}
				else if (property.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)))
				{
					return new DataTypeAttribute(DataTypes.SelectMultiple);
				}
				else if (System.Text.RegularExpressions.Regex.IsMatch(property.PropertyType.FullName, @"^(System.Nullable`1\[\[)?\b(System.Byte|System.SByte|System.UInt16|System.UInt32|System.UInt64|System.Int16|System.Int32|System.Int64|System.Decimal|System.Double|System.Single)\b"))
				{
					return new DataTypeAttribute(DataTypes.Number);
				}
			}

			return dataType;
		}

		/// <summary>
		/// Criar um grupo de propriedades através do atributo Pesquisa encontrado nas propriedades da viewmodel.
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<PropertyInfo> GridProperties()
		{
			foreach (PropertyInfo item in GetReflectedType().GetProperties())
			{
				if (item.GetGetMethod().IsVirtual)
					continue; //não é possível fazer a inclusão de propriedades virtuais

				dynamic group = item.GetCustomAttributes().FirstOrDefault(x => x.ToString().EndsWith(".PesquisaAttribute"));  //não é possível pegar o atributo tipado, pois ele não faz parte do projeto de scaffolding
				if (group != null && group.Grid)
					yield return item;
			}
		}

		/// <summary>
		/// Cria um grupo de propriedades que irão ser usadas para fazer filtros.
		/// </summary>
		/// <param name="grid">Listagem de propriedades que serão filtradas</param>
		/// <returns></returns>
		public static IEnumerable<PropertyInfo> PesquisaProperties(IEnumerable<PropertyInfo> grid)
		{
			foreach (PropertyInfo item in grid)
			{
				if (item.GetGetMethod().IsVirtual)
					continue; //não é possível fazer a inclusão de propriedades virtuais

				dynamic group = item.GetCustomAttributes().FirstOrDefault(x => x.ToString().EndsWith(".PesquisaAttribute"));  //não é possível pegar o atributo tipado, pois ele não faz parte do projeto de scaffolding
				if (group != null && group.Pesquisa)
					yield return item;
			}
		}

		/// <summary>
		/// Obter o objeto original ViewModel.
		/// </summary>
		/// <returns>Tipo reflexivo</returns>
		public static Type GetReflectedType()
		{
			if (ModoTeste)
			{
				Type tref = Referencia;
				return (Assembly.GetAssembly(tref)).GetType($"ICICore.Teste.Web.ViewModels.{ModelRef.ViewDataTypeShortName}");
			}
			else
			{
				Type tref = Type.GetType("ICICore.Web.Startup");
				return (Assembly.GetAssembly(tref)).GetType($"ICICore.Web.ViewModels.{ModelRef.ViewDataTypeShortName}");
			}
		}

		/// <summary>
		/// Verifica se uma propriedade contém relacionamentos com outras.
		/// </summary>
		/// <param name="property">Ligação para propriedade reflexiva do modelo</param>
		/// <returns>true, se for chave estrangeira; false, caso contrário</returns>
		public static bool IsForeignKey(PropertyInfo property)
		{
			var chave = property?.GetCustomAttributes(typeof(ForeignKeyAttribute), true).FirstOrDefault() as ForeignKeyAttribute;
			return chave != null;
		}

		/// <summary>
		/// Verifica se uma propriedade é um enumerador.
		/// </summary>
		/// <param name="property">Ligação para propriedade reflexiva do modelo</param>
		/// <returns></returns>
		public static bool IsEnum(PropertyInfo property)
		{
			return property.PropertyType.IsEnum;
		}

		/// <summary>
		/// Verifica se uma propriedade é um enumerador com flags.
		/// </summary>
		/// <param name="property">Ligação para propriedade reflexiva do modelo</param>
		/// <returns></returns>
		public static bool IsEnumFlags(PropertyInfo property)
		{
			return property.PropertyType.GetCustomAttribute<FlagsAttribute>() != null;
		}

		/// <summary>
		/// Verifica se uma propriedade pode ser usada para scaffolding.
		/// </summary>
		/// <param name="property">Ligação para propriedade reflexiva do modelo</param>
		/// <returns></returns>
		public static bool IsScaffold(PropertyInfo property)
		{
			return property.GetCustomAttribute<ScaffoldColumnAttribute>()?.Scaffold ?? true;
		}

		/// <summary>
		/// Verifica se uma propriedade pode ser usada para scaffolding.
		/// </summary>
		/// <param name="property">Ligação para propriedade reflexiva do modelo</param>
		/// <returns></returns>
		public static bool IsPrimaryKey(PropertyInfo property)
		{
			return property.GetCustomAttribute<KeyAttribute>() != null;
		}

		/// <summary>
		/// Obter SubGrupo, informação para gerar os subgrupos.
		/// </summary>
		/// <param name="property">Propriedade Entityframework</param>
		/// <returns>PropertyInfo</returns>
		public static SubGrupoAttribute GetDisplayGroup(PropertyInfo property)
		{
			try
			{    //ocorrerá erro se não achar o atributo
				var display = property.GetCustomAttributes().FirstOrDefault(x => x.ToString().EndsWith(".SubGrupoAttribute"));    //não é possível pegar o atributo tipado, pois ele não faz parte do projeto de scaffolding
				return (display != null) ? (SubGrupoAttribute)display : null;
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// Procura a propriedade complementar, que é utilizada nos relacionamentos.
		/// Normalmente a propriedade complementar tem o nome da propriedade principal, sem sufixo 'Id' e com atributo 'virtual'.
		/// </summary>
		/// <returns></returns>
		public static PropertyInfo GetRelatedProperty(PropertyInfo property)
		{
			foreach (var prop in property.DeclaringType.GetProperties())
			{
				if (prop.GetGetMethod().IsVirtual && prop.Name + "Id" == property.Name)
				{
					return prop;
				}
			}
			throw new Exception("Não foi encontrada a propriedade relacionada.");
		}

		/// <summary>
		/// Criar um grupo de propriedades através do atributo Grupo encontrado nas propriedades da viewmodel.
		/// Retorna também uma lista de itens sem agrupamento.
		/// </summary>
		/// <returns></returns>
		public static Dictionary<string, List<PropertyInfo>> GroupProperties()
		{
			var groups = new Dictionary<string, List<PropertyInfo>>();

			foreach (var prop in GetReflectedType().GetProperties())
			{
				var display = prop.GetCustomAttribute<DisplayAttribute>() ?? new DisplayAttribute() { GroupName = "" };
				var hidden = prop.GetCustomAttribute<HiddenInputAttribute>() ?? new HiddenInputAttribute() { DisplayValue = true };

				if (prop.GetGetMethod().IsVirtual)
					continue;
				if (display.GroupName == null)    //não pode haver chave nula
				{
					display.GroupName = "";
				}
				if (!IsScaffold(prop))
				{
					display.GroupName = "";
				}
				if (!hidden.DisplayValue)
				{
					display.GroupName = "";
				}

				if (!groups.Keys.Contains(display.GroupName)) {
					groups[display.GroupName] = new List<PropertyInfo>();
				}
				groups[display.GroupName].Add(prop);
			}
			return groups;
		}

		/// <summary>
		/// Verificar se a propriedade é somente para leitura.
		/// </summary>
		/// <param name="property">Ligação para propriedade reflexiva do modelo</param>
		/// <returns></returns>
		public static bool IsReadOnly(PropertyInfo property)
		{
			return property.GetCustomAttribute<ReadOnlyAttribute>()?.IsReadOnly ?? false;
		}

		/// <summary>
		/// Verificar se a propriedade é somente para leitura.
		/// </summary>
		/// <param name="property">Ligação para propriedade reflexiva do modelo</param>
		/// <returns></returns>
		public static bool IsAutoGenerated(PropertyInfo property)
		{
			//impossível saber sem o modelo de dados.
			return false; 
		}

		/// <summary>
		/// This function converts the primary key short type name to its nullable equivalent when possible. This is required to makesure that an 
		/// HTTP 400 error is thrown when the user tries to access the edit, delete, or details action with null values.
		/// </summary>
		/// <param name="typeName"></param>
		/// <param name="shortTypeName"></param>
		/// <returns></returns>
		public static string GetNullableTypeName(string typeName, string shortTypeName)
		{
			// The exceptions are caught because if for any reason the type is user defined, then the short type name will be used.
			// In that case the user will receive a server error if null is passed to the edit, delete, or details actions.
			Type primaryKeyType = null;
			try
			{
				primaryKeyType = Type.GetType(typeName);
			}
			catch { }
			if (primaryKeyType != null && (TypeUtilities.IsTypePrimitive(primaryKeyType) || typeName == "System.Guid"))
			{
				return shortTypeName + "?";
			}
			return shortTypeName;
		}

		/// <summary>
		/// Faz uma varredura no objeto de modelo para buscar chaves primárias.
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<PropertyInfo> GetPrimaryKeys()
		{
			foreach (var prop in GetReflectedType().GetProperties())
			{
				var keyAttr = prop.GetCustomAttribute<KeyAttribute>();
				if (keyAttr != null)
				{
					yield return prop;
				}
			}
		}

		/// <summary>
		/// Verifica se o modelo possui uma chave primária.
		/// </summary>
		/// <returns></returns>
		public static bool HasPrimaryKey()
		{
			foreach (var prop in GetReflectedType().GetProperties())
			{
				var keyAttr = prop.GetCustomAttribute<KeyAttribute>();
				if (keyAttr != null)
					return true;
			}
			return false;    //passou por todas as propriedades e não achou chave primária.
		}

		/// <summary>
		/// Verifica se na listagem de propriedade/controle informada há pelo menos um controle visível.
		/// Nota: Se não houver um controle visível, é interessante ocultar o grupo todo.
		/// </summary>
		/// <param name="list">Lista de propriedades reflexivas.</param>
		/// <returns>Retorna 'true' se houver pelo menos um controle visível. E 'false' se não tiver nenhum controle visível.</returns>
		public static bool GroupWithAnyVisible(IEnumerable<PropertyInfo> list)
		{
			foreach (var prop in list)
			{
				var hidden = prop.GetCustomAttribute<HiddenInputAttribute>() ?? new HiddenInputAttribute() { DisplayValue = true };

				if (IsScaffold(prop) && hidden.DisplayValue)
				{
					return true;
				}
			}
			return false;    //todos ocultos/omitidos
		}
	}
}
