//The function
function moveString(str){
str=str.slice(-1)+str.slice(0,-1);
return str;
}
var theString="Hello,World";
for(let i=0;i<theString.length;i++)
{
theString=moveString(theString);
console.log(theString);
}


//Opettajan tapa
function mySleep(secs) {
var waitTill = new Date(new Date().getTime() + secs * 1000);
while (waitTill > new Date());
}
var theString="Hello,World";
for(let i=0;i<theString.length;i++)
{
theString=moveString(theString);
console.log(theString);
mySleep(1);
}

//Async
async function sleepAsync(secs){
var waitTill = new Date(new Date().getTime() + secs * 1000);
while (waitTill > new Date());
}
var theString="Hello,World";
for(let i=0;i<theString.length;i++)
{
theString=moveString(theString);
console.log(theString);
sleepAsync(1).then();
}

//Async await
async function sleepAsync(secs){
var waitTill = new Date(new Date().getTime() + secs * 1000);
while (waitTill > new Date());
}
var theString="Hello,World";
(async function AsyncAwait(){
for(let i=0;i<theString.length;i++)
{
theString=moveString(theString);
console.log(theString);
await sleepAsync(1);
}
})();

//Why not setInterval?
var theString="Hello,World";
var i=0;
var interval=setInterval(function(){
theString=moveString(theString);
console.log(theString);
i++;
if(i>=theString.length) clearInterval(interval);
},1000);

//Better way than while loop
var theString="Hello,World";
var a=async(secs,str)=>{
for(let i=0;i<theString.length;i++)
{
str=moveString(str);
console.log(str);
await new Promise(resolve=>setTimeout(resolve,secs*1000));
}
}
a(1,theString);

//Just WAT way
var theString="Hello,World";
var str="";
for(let i=0;i<theString.length;i++){
str+="setTimeout(()=>{theString=moveString(theString);console.log(theString);";
}
for(let i=0;i<theString.length;i++){
str+="},1000)";
}
eval(str);