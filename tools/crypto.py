from cryptography.hazmat.primitives.ciphers import Cipher, algorithms, modes
import base64
import os
import binascii

# HEX string to bytes conversion
def hex_to_bytes(hex_string: str) -> bytes:
    return binascii.unhexlify(hex_string)

# KEY32 Replace with actual key bytes
# IV16 Replace with actual IV bytes

TOKEN = "u8mseFvy2H5L"
TOKEN = """u8mseFvy2H5L

version: 1.0.0

Ahoy, ye explorers!

I be mighty impressed ye’ve stumbled upon this here file. I was plannin' to bury it down, but thought it'd be a jolly good time to leave a message for ye.

This here problem be crafted for curious computer swashbucklers. A fine crew like yerself should be able to uncover it, aye? If ye can't, well shiver me timbers, ye might not be the scallywag I’m searchin' for.

I hope ye have a grand time readin’ the code. Should ye have any questions, don’t hesitate to holler.

Tell me yer grand tale and picture the treasure ye've uncovered.
Fair winds and good luck!

- cs3.ustp@gmail.com

"""

KEY1 = '2b7e4ab34ccc5131e6050205483200f580c04f5b36a0a3719b851cd092985080'
IV1 = '0d75cfcfb4bf31bd700ec5075df553a6'

KEY2 = '093304f82a1466ca92c3f129378b17fb016ee42bb4269a2ed50fc6fa7a8c9f43'
IV2 = '7572ecc622c8d9b74320ed9cc1993127'

KEY1_BYTES = hex_to_bytes(KEY1)
IV1_BYTES = hex_to_bytes(IV1)
KEY2_BYTES = hex_to_bytes(KEY2)
IV2_BYTES = hex_to_bytes(IV2)

def encrypt(plain_text: str, key: bytes, iv: bytes) -> str:
    cipher = Cipher(algorithms.AES(key), modes.CFB(iv))
    encryptor = cipher.encryptor()
    encrypted_bytes = encryptor.update(plain_text.encode('utf-8')) + encryptor.finalize()
    encrypted_base64 = base64.b64encode(encrypted_bytes).decode('utf-8')
    return encrypted_base64

def decrypt(encrypted_base64: str, key: bytes, iv: bytes) -> str:
    encrypted_bytes = base64.b64decode(encrypted_base64)
    cipher = Cipher(algorithms.AES(key), modes.CFB(iv))
    decryptor = cipher.decryptor()
    decrypted_bytes = decryptor.update(encrypted_bytes) + decryptor.finalize()
    plain_text = decrypted_bytes.decode('utf-8')
    return plain_text

def rot13(text: str) -> str:
    result = []
    for char in text:
        if 'a' <= char <= 'z':
            offset = ord('a')
            result.append(chr((ord(char) - offset + 13) % 26 + offset))
        elif 'A' <= char <= 'Z':
            offset = ord('A')
            result.append(chr((ord(char) - offset + 13) % 26 + offset))
        else:
            result.append(char)
    return ''.join(result)

# Encrypt with KEY1 and IV1
encrypted_text = encrypt(TOKEN, KEY1_BYTES, IV1_BYTES)

# Apply ROT13 encoding
encrypted_text_rot13 = rot13(encrypted_text)

# Encrypt with KEY2 and IV2
encrypted_text = encrypt(encrypted_text_rot13, KEY2_BYTES, IV2_BYTES)

# Apply ROT13 encoding again
encrypted_text_rot13 = rot13(encrypted_text)

print(f"Encrypted: {encrypted_text_rot13}")


# decrypt
# Apply ROT13 decoding
decrypted_text = rot13(encrypted_text_rot13)

# Decrypt with KEY2 and IV2
decrypted_text = decrypt(decrypted_text, KEY2_BYTES, IV2_BYTES)

# Apply ROT13 decoding again
decrypted_text = rot13(decrypted_text)

# Decrypt with KEY1 and IV1
decrypted_text = decrypt(decrypted_text, KEY1_BYTES, IV1_BYTES)

# print(f"{decrypted_text}")

