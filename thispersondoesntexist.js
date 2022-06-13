
function reImage(){
	let loader=document.getElementById("loader_box");
	loader.classList.toggle('active');
	let xhr=new XMLHttpRequest();
	let timeInMs=Date.now();
	xhr.open('GET','/en?new='+timeInMs);
	xhr.responseType='json';
	xhr.send();
	xhr.onload=function(){
		let responseObj=xhr.response;
		console.log(responseObj);
		let img=document.getElementById("avatar");
		img.setAttribute("src",responseObj.src);
		img.onload=function(){loader.classList.toggle('active')}
	}}