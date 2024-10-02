How to play?
WASD - moving
mouse - rotation
L.Shift - sprint
E - interaction (technically it's just opening the gate, because there are only one interaction)
Collect all keys, don't die from enemies and find exit gate.

What's under the hood?
Camera: I don't attach camera to char because it causes some isuue, so i just put camera to exact position of placeholder on character.
GameManager: I have put too much on GameManager (spawn, background music, ui stuff). It causes mess in code and hard-resolving dependencies
Interactions: They are not scaleable right now. It could fixed by tweaking tip displaying and adding interaction types.
