<template>
    <div class="modal show" style="display: block;" v-if="isVisible">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Bekräfta borttagning</h5>
                    <button type="button" class="btn-close" @click="closeModal"></button>
                </div>
                <div class="modal-body">
                    <p>Är du säker på att du vill radera detta evenemang?</p>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-danger" @click="confirmDelete">Radera</button>
                    <button class="btn btn-secondary" @click="closeModal">Avbryt</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';

export default defineComponent({
    name: 'ConfirmDeleteModal',
    props: {
        isVisible: {
            type: Boolean,
            required: true,
        },
        eventId: {
            type: Number,
            required: true,
        },
    },
    emits: ['confirm', 'cancel'],
    setup(props, { emit }) {
        // Funktion för att bekräfta borttagning
        const confirmDelete = () => {
            emit('confirm', props.eventId);
        };

        // Funktion för att stänga modalen
        const closeModal = () => {
            emit('cancel');
        };

        return {
            confirmDelete,
            closeModal,
        };
    },
});
</script>

<style>
    /* Lägg till din CSS här om du vill */
</style>
