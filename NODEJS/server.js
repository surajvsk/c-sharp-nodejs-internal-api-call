const express = require('express');
const path = require('path');
const bodyParser = require('body-parser')


const port = 5000;
const app = express();
const router = express.Router()

app.use(express.json())
app.use(express.urlencoded({ extended: false}));



app.use(express.static(path.join(__dirname, 'public')));


app.post('/sendme', function(req, res, next){
	
	console.log('REQ::::::::::::::', req.body)
	
	res.json({status:200, message:"SEND FROM SERVER"})
});


router.get('/getme', function(req, res, next){
	console.log('REQ::::::::::::::', req.body.encrypted)
	res.json({status:200, message:"HELLO MELINDA"})
	
});


app.listen(port);