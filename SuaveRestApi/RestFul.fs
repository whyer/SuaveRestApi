namespace SuaveRestApi.Rest

[<AutoOpen>] 
module RestFul =
  open Newtonsoft.Json
  open Newtonsoft.Json.Serialization
  open Suave
  open Suave.Successful
  open Suave.Operators
  open Suave.Filters

  type RestResource<'a> = {
    GetAll : unit -> 'a seq
    Create : 'a -> 'a
   }

  let JSON v =
    let settings = new JsonSerializerSettings()
    settings.ContractResolver <- new CamelCasePropertyNamesContractResolver()
    let json = JsonConvert.SerializeObject(v, settings)
    json |> OK >=> Writers.setMimeType "application/json"

  let fromJson<'a> json = 
    JsonConvert.DeserializeObject(json, typeof<'a>) :?> 'a

  let getResourceFromReqest<'a> (req : HttpRequest) =
    let getString rawForm =
      System.Text.Encoding.UTF8.GetString(rawForm)
    req.rawForm |> getString |> fromJson<'a>

   //	string	->	RestResource<'a>	->	WebPart
  let rest resourceName resource =
    let resourcePath = "/" + resourceName
    let getAll = warbler (fun _ -> resource.GetAll () |> JSON)
    path resourcePath >=> choose [ 
      GET >=> getAll
      POST >=> request (getResourceFromReqest >> resource.Create >> JSON)
    ]

  