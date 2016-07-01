open SuaveRestApi.Rest
open SuaveRestApi.Db
open Suave
open Suave.Web

[<EntryPoint>]
let main argv = 
    let personWebPart = rest "person" {
      GetAll = Db.getPeople
      Create = Db.createPerson
      Update = Db.updatePerson
      Delete = Db.deletePersonById
    }

    startWebServer defaultConfig personWebPart
    0 
