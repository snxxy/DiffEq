import { defineComponent } from 'vue'

export default defineComponent({
    props: {},
    emits: {
        buttonPressEmit: null,
    },
    setup(props, { emit }) {
        const buttonPressed = (pressedCode) => {
            emit('buttonPressEmit', pressedCode)
        }

        return {
            buttonPressed
        }
    },
    template: "#nav-button-bar-template"
})