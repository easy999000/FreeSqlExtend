# FreeSqlExtend
1.appsettings.json 添加配置节点  
        "FreeSqlExtend": {
            "DBConnectionList": [
              {
                "Name": "cms",
                "ConnectionStr": "Server=172.168.16.1*****************************",
                "DbType": "MySql"
              },
              {
                "Name": "shop",
                "ConnectionStr": "Server=172.168.16.1*****************************",
                "DbType": "MySql",
                "SlaveConnectionStr": [
                  "Server=172.168.16.1*****************************",
                  "Server=172.168.16.1*****************************",
                  "Server=172.168.16.1*****************************"
                ]
              }
            ]
          }

2.startup中初始化服务

          ////初始化数据库访问层
          var serviceProvider = services.BuildServiceProvider();
          FreeSqlHelperStatic.InitStaticDB(new SettingHelper(serviceProvider.GetService<IConfiguration>())); 

3.如果是多数据库系统,可以定义一个之类,方便多库操作.提高效率,不定义不影响使用.
          public class SqlHelper : FreeSqlHelperStatic
          {
              public static IFreeSql cms
              {
                  get
                  {
                      return StaticDB.GetDB("cms");
                  }
              }
              public static IFreeSql shop
              {
                  get
                  {
                      return StaticDB.GetDB("shop");
                  }
              }
          }
          
4.配置完成,可以使用 SqlHelper或FreeSqlHelperStatic的静态方法,访问数据库了.
   
          
          
