import { describe, it, expect, vi } from 'vitest'
import { render, screen, fireEvent } from '@testing-library/react'
import { ChatSuggestionChips } from './ChatSuggestionChips'

describe('ChatSuggestionChips', () => {
  it('renders suggested header and option chips', () => {
    const onChipClick = vi.fn()
    render(<ChatSuggestionChips role="engineering" onChipClick={onChipClick} isTyping={false} />)

    // Verify it renders suggestion header
    expect(screen.getByText(/suggested questions/i)).toBeInTheDocument()

    // Engineering chips should render (e.g. "Who is my buddy?")
    const buttons = screen.getAllByRole('button')
    expect(buttons.length).toBeGreaterThan(0)
  })

  it('invokes callback when a chip is clicked', () => {
    const onChipClick = vi.fn()
    render(<ChatSuggestionChips role="engineering" onChipClick={onChipClick} isTyping={false} />)

    const buttons = screen.getAllByRole('button')
    fireEvent.click(buttons[0])

    expect(onChipClick).toHaveBeenCalledTimes(1)
  })

  it('disables chips when isTyping is true', () => {
    const onChipClick = vi.fn()
    render(<ChatSuggestionChips role="engineering" onChipClick={onChipClick} isTyping={true} />)

    const buttons = screen.getAllByRole('button')
    buttons.forEach(button => {
      expect(button).toBeDisabled()
    })
  })
})
