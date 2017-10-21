param(
[string]$version)

#Validate parameters
if (!$version)
{
    Write-Host "Parameter: version is mandatory"
    exit
}

$image_name = "stockquoteservice"
$image_name_current_version = $image_name + ":" + $version
$image_name_latest_version = $image_name + ":latest"


#Stop old version and delete container
docker rm $(docker stop $(docker ps -a -q --filter ancestor=$image_name --format="{{.ID}}"))

#Build stock quote service
dotnet publish --configuration release

#Create container
docker build -t $image_name_current_version .\bin\release\netcoreapp1.0\publish
docker build -t $image_name_latest_version .\bin\release\netcoreapp1.0\publish

#Run container
docker run --restart=always -d -p 9900:80 $image_name_latest_version

#TODO
#-Create a new web get in api api project maybe with datefilter, or last 10 quotations
#-Invoke-RestMethod http://localhost:5000/api/token
# Test is result is there and working

#---Add docker swarm https://github.com/stefanprodan/aspnetcore-dockerswarm/wiki/Stateless-microservice-scaling