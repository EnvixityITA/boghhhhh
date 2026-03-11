const scene = new THREE.Scene();

const camera = new THREE.PerspectiveCamera(
75,
window.innerWidth/window.innerHeight,
0.1,
1000
);

const renderer = new THREE.WebGLRenderer();
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

camera.position.z = 20;

// Pianeta
const planetGeometry = new THREE.SphereGeometry(5, 64, 64);
const planetMaterial = new THREE.MeshStandardMaterial({color:0x2266ff});
const planet = new THREE.Mesh(planetGeometry, planetMaterial);

scene.add(planet);

// luce
const light = new THREE.PointLight(0xffffff, 2);
light.position.set(20,20,20);
scene.add(light);

let asteroids = [];

function spawnAsteroid() {

const geo = new THREE.SphereGeometry(0.5,16,16);
const mat = new THREE.MeshStandardMaterial({color:0x888888});
const asteroid = new THREE.Mesh(geo,mat);

const dir = new THREE.Vector3(
Math.random()*2-1,
Math.random()*2-1,
Math.random()*2-1
).normalize();

asteroid.position.copy(dir.multiplyScalar(20));

asteroid.userData.velocity = dir.multiplyScalar(-0.1);

asteroids.push(asteroid);
scene.add(asteroid);
}

document.addEventListener("click", spawnAsteroid);

function animate() {

requestAnimationFrame(animate);

planet.rotation.y += 0.002;

asteroids.forEach(a=>{
a.position.add(a.userData.velocity);
});

renderer.render(scene,camera);

}

animate();
