using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referência ao jogador
    public Vector3 offset;   // Offset para ajustar a posição da câmera em relação ao jogador

    void Update()
    {
        if (player != null)
        {
            // Atualiza a posição da câmera com base na posição do jogador e no offset
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
        }
    }
}
