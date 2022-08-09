# C-Steganography-2.0

STEGANOGRAPHY PROGRAM: USER GUIDE

By: Kenny Tran

Steganography is the practice of hiding a secret message in something that is not a secret. To achieve this, The Secret Encryption program that I've created will hide a user's message inside of an image by converting each character into pixel bytes. The Secret Decoder program will decipher a user's message and display the results. 

Notes:

- The decoder and encoder are specifically built to hide important messages within images of different color values. Blue images are ideal in encrypting messages as they have the least intensity amongst the RGB values. A common algorithm known as the least significant bit (LSB) was used to create the effect of hiding a user's message in an image without notice of the human. It replaces the 3 leftmost bits in each color pixel's byte while not significantly altering the actual image.  

- This program has been built to display Portable Pixel Map (PPM) Files Only. This means that ppm images will ONLY be able to open and save on the encryption program, while the decoder will only read PPM images. For the sake of the project, test images were converted to ascii format (P3) and raw format (P6) on GIMP. You can then open and overwrite the image with your encrypted message. No image downloads necessary. 

- Encrypted messages should not exceed more than 256 characters.

- Some images may be too small to fit 256 characters, so it is recommended a medium to large image is selected for encryption. I've placed some input validations to check for this.

- Only letters and numbers will display, while punctuation and other characters will not be displayed in the decoding. 

- During the encryption process, messages are reversed as to further protect against detection.

HAPPY CODING!
