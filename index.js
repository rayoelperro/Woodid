const app = require('express')();
const http = require('http').Server(app);
const io = require('socket.io')(http);
const scs = require('screenshot-desktop');
const exec = require('child_process').exec;
const port = 8080;

function capture(doimg, onerr){
  scs().then((img) => {
    var datajpg = "data:image/jpeg;base64," + img.toString('base64');
    doimg(datajpg);
  }).catch((err) => {
    onerr(err.message);
  });
}

function execute(command){
  exec(command, function (err, stdout, stderr) {
    if(err){
      console.log("Error: " + err.message);
      return;
    }
    if(stdout != "Ok") console.log(command + ":: " + stdout);
  });
}

app.set('view engine', 'ejs');

app.get('/', function (req, res) {
  capture(function (img64) {
    res.render("main.ejs",{source:img64});
  }, function (message) {
    res.send("There was an error: " + message);
  });
});

io.on('connection', function (socket) {
  console.log("A user connected");
  socket.on('disconnect', function () {
    console.log("A user disconnected");
  })
  socket.on('mouse', function (msg) {
    console.log(msg);
    execute(__dirname + "/tools/control.exe " + msg);
  });
  setInterval(function () {
    capture(function (img64) {
      io.emit('img', img64);
    }, function (message) {
      io.emit('error', message);
    });
  },60);
});

http.listen(port, function () {
  console.log("Running on: " + port + " (CNTRL+C : EXIT)");
})
