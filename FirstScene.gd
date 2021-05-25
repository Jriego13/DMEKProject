extends ColorRect

var eye1 = preload("res://eye1.png")
var eye2 = preload("res://eye2.png")
var toggle = false

onready var button = $TestButton
onready var eye = $EyeImage


func _ready():
	button.connect("pressed", self, "_button_pressed")
	
func _button_pressed():

	if (toggle == false):
		print("here")
		eye.set_texture(eye2)
		toggle = true
	else:
		print("in here")
		eye.set_texture(eye1)
		toggle = false
	
