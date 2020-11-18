
let main = document.querySelector("main");
let frag = document.createDocumentFragment();
let div = document.createElement("div");
div.style.width = document.querySelector("article").offsetWidth + "px";
let imagesList = [...document.getElementsByTagName("article")].map(item=>item.getElementsByTagName("img")[0]).filter(img=>img).map(img=>img.src);
imagesList.unshift(main.querySelector("img").src); //own picture first
let images = new Set(imagesList);
let imgStyle = document.createElement("img");
imgStyle.style.cssText = "width:50px; height:50px; border-radius:50%; margin:2px 6px; padding: 3px; background-image: linear-gradient( 45deg, orange, red);"
for(let img of images) {
   let cloned = imgStyle.cloneNode();
   cloned.src = img;
   frag.appendChild(cloned);
}
div.appendChild(frag);
main.insertBefore(div, main.firstElementChild);
