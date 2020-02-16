extends Node


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	 # Replace with function body.
	var music__ = AudioStreamPlayer.new()
	self.add_child(music__)
	music__.stream = load("res://assets/sprites/soundtrack.ogg")
	music__.play()

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
